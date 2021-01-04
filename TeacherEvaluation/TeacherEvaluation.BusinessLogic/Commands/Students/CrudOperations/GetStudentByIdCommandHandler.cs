using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentByIdCommandHandler : IRequestHandler<GetStudentByIdCommand, Student>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudentByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Student> Handle(GetStudentByIdCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.Exists(x => x.Id == request.Id);
            if (studentExists)
            {
                return await unitOfWork.StudentRepository.GetStudent(request.Id);
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
