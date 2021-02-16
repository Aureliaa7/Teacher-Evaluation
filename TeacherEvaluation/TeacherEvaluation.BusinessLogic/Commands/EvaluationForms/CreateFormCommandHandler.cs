using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

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
                Type = request.FormType 
            };
            await unitOfWork.FormRepository.Add(form);
            foreach (var question in request.Questions)
            {
                Question newQuestion = new Question
                {
                    Text = question,
                    Form = form
                };
                await unitOfWork.QuestionRepository.Add(newQuestion);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
