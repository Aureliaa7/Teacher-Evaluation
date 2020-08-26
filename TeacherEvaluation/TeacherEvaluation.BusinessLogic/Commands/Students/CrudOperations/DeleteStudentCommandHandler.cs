using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class DeleteStudentCommandHandler : AsyncRequestHandler<DeleteStudentCommand>
    {
        private readonly IStudentRepository studentRepository;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        protected override async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.Id);
            if (studentExists)
            {
                await studentRepository.Delete(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The student was not found...");
            }         
        }
    }
}
