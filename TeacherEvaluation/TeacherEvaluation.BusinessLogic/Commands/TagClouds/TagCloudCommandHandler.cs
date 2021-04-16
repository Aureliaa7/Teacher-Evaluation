using MediatR;
using Sparc.TagCloud;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.TagClouds
{
    public class TagCloudCommandHandler : IRequestHandler<TagCloudCommand, IEnumerable<TagCloudTag>>
    {
        private readonly IUnitOfWork unitOfWork;

        public TagCloudCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TagCloudTag>> Handle(TagCloudCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.Exists(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            IEnumerable<TagCloudTag> tags = new List<TagCloudTag>();

            if (formExists && teacherExists)
            {
                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId))
                                .TakeLast(Constants.NumberOfFreeFormQuestions)                                                
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
                tags = tags.Shuffle();
            }

            return tags;
        }
    }
}
