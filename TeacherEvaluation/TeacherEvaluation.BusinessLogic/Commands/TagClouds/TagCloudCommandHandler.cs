using MediatR;
using Microsoft.AspNetCore.Hosting;
using Sparc.TagCloud;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

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
            bool formExists = await unitOfWork.FormRepository.Exists(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            IEnumerable<TagCloudTag> tags = new List<TagCloudTag>();

            if (formExists && teacherExists)
            {
                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId))
                                .Where(q => q.HasFreeFormAnswer)
                                .ToList();

                var freeFormTexts = new List<string>();

                for (int contor = 0; contor < Constants.NumberOfFreeFormQuestions; contor++)
                {
                    var responses = (await unitOfWork.AnswerToQuestionWithTextRepository.GetByQuestionId(questions.ElementAt(contor).Id))
                                     .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == request.TeacherId);

                    var selectedResponses = responses.Select(x => x.FreeFormAnswer);
                    freeFormTexts.AddRange(selectedResponses);
                }

                var analyzer = new TagCloudAnalyzer();
                tags = analyzer.ComputeTagCloud(freeFormTexts);
                tags = RemoveCommonWords(tags.ToList());
                tags = tags.Shuffle();
            }

            return tags;
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
    }
}
