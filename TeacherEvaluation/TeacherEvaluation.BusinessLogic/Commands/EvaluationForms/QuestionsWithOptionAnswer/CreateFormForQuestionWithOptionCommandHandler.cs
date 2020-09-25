using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class CreateFormForQuestionWithOptionCommandHandler : AsyncRequestHandler<CreateFormForQuestionWithOptionCommand>
    {
        private readonly IRepository<Form> formRepository;
        private readonly IRepository<QuestionWithOptionAnswer> questionRepository;

        public CreateFormForQuestionWithOptionCommandHandler(IRepository<Form> formRepository, IRepository<QuestionWithOptionAnswer> questionRepository)
        {
            this.formRepository = formRepository;
            this.questionRepository = questionRepository;
        }

        protected override async Task Handle(CreateFormForQuestionWithOptionCommand request, CancellationToken cancellationToken)
        {
            Form form = new Form
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EnrollmentState = request.EnrollmentState,
                Type = FormType.Option, 
                MinNumberOfAttendances = request.MinNumberAttendances
            };
            await formRepository.Add(form);

            foreach(var question in request.Questions)
            {
                QuestionWithOptionAnswer newQuestion = new QuestionWithOptionAnswer
                {
                    Question = question,
                    Form = form
                };
                await questionRepository.Add(newQuestion);
            }
        }
    }
}
