using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateFormCommandHandler : AsyncRequestHandler<CreateFormCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateFormCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            Form form = new Form
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EnrollmentState = EnrollmentState.InProgress,
                MinNumberOfAttendances = request.MinNumberOfAttendances
            };
            await unitOfWork.FormRepository.AddAsync(form);

            var freeFormQuestions = request.Questions.TakeLast(Constants.NumberOfFreeFormQuestions);
            await SaveQuestions(freeFormQuestions, true, form);
            var questionsWithAnswerOption = request.Questions.Take(Constants.NumberOfLikertQuestions);
            await SaveQuestions(questionsWithAnswerOption, false, form);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task SaveQuestions(IEnumerable<string> questions, bool haveTextAnswer, Form form)
        {
            foreach (var question in questions)
            {
                Question newQuestion = new Question
                {
                    Text = question,
                    Form = form,
                    HasFreeFormAnswer = haveTextAnswer
                };
                await unitOfWork.QuestionRepository.AddAsync(newQuestion);
            }
        }
    }
}
