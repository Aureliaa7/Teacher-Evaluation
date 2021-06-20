using MediatR;
using Microsoft.AspNetCore.Hosting;
using Sparc.TagCloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TagClouds
{
    public class TagCloudCommandHandler : IRequestHandler<TagCloudCommand, IEnumerable<TagCloudTag>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TagCloudCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<TagCloudTag>> Handle(TagCloudCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.ResponseRetrievalCriteria.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.ResponseRetrievalCriteria.TeacherId);
            IEnumerable<TagCloudTag> tags = new List<TagCloudTag>();

            if (formExists && teacherExists)
            {
                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntitiesAsync(request.ResponseRetrievalCriteria.FormId))
                                .Where(q => q.HasFreeFormAnswer)
                                .ToList();

                IEnumerable<string> freeFormTexts = new List<string>();
                if (request.ResponseRetrievalCriteria.TaughtSubjectId.Equals("All"))
                {
                    freeFormTexts = await GetTagCloudDataOverallAsync(request.ResponseRetrievalCriteria.TeacherId, questions);
                }
                // if the "Please select" option is selected instead of the subject
                else if (request.ResponseRetrievalCriteria.TaughtSubjectId.Equals("default"))
                {
                }
                else if (await unitOfWork.TaughtSubjectRepository.ExistsAsync(ts => ts.Id == new Guid(request.ResponseRetrievalCriteria.TaughtSubjectId)))
                {
                    freeFormTexts = await GetTagCloudDataForTaughtSubjectAsync(request.ResponseRetrievalCriteria.TeacherId, 
                        new Guid(request.ResponseRetrievalCriteria.TaughtSubjectId), questions);
                }

                var analyzer = new TagCloudAnalyzer();
                tags = analyzer.ComputeTagCloud(freeFormTexts);
                tags = RemoveCommonWords(tags.ToList());
                tags = tags.OrderBy(t => t.Category).Take(Constants.MaxNumberOfTagsInWordCloud);
                tags = tags.Shuffle();

                return tags;
            }
            throw new ItemNotFoundException("The form or the teacher was not found...");
        }

        private IList<TagCloudTag> RemoveCommonWords(IList<TagCloudTag> tags)
        {
            var commonWords = GetCommonWords();
            var newTags = new List<TagCloudTag>();

            foreach (var tag in tags)
            {
                if (!commonWords.Contains(tag.Text))
                {
                    newTags.Add(tag);
                }
            }

            return newTags;
        }

        private IEnumerable<string> GetCommonWords()
        {
            string blackListPath = Path.Combine(webHostEnvironment.WebRootPath, "tagCloud", "blackList.txt");
            var commonWords = new List<string>();
            using (StreamReader reader = new StreamReader(blackListPath))
            {
                while (!reader.EndOfStream)
                {
                    commonWords.AddRange(reader.ReadLine().Split(','));
                }
            }

            return commonWords;
        }

        private async Task<IEnumerable<string>> GetTagCloudDataOverallAsync(Guid teacherId, List<Question> questions)
        {
            var freeFormTexts = new List<string>();

            foreach (var question in questions)
            {
                freeFormTexts.AddRange((await unitOfWork.AnswerToQuestionWithTextRepository.GetByQuestionIdAsync(question.Id))
                                 .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == teacherId)
                                 .Select(x => x.FreeFormAnswer)
                                 .ToList());
            }

            return freeFormTexts;
        }

        private async Task<IEnumerable<string>> GetTagCloudDataForTaughtSubjectAsync(Guid teacherId, Guid taughtSubjectId, List<Question> questions)
        {
            var freeFormTexts = new List<string>();

            foreach (var question in questions)
            {
                freeFormTexts.AddRange((await unitOfWork.AnswerToQuestionWithTextRepository.GetByQuestionIdAsync(question.Id))
                                 .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == teacherId &&
                                        r.Enrollment.TaughtSubject.Id == taughtSubjectId)
                                 .Select(x => x.FreeFormAnswer)
                                 .ToList());
            }

            return freeFormTexts;
        }
    }
}