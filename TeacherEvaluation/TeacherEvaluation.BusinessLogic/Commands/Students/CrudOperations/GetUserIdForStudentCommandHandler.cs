using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetUserIdForStudentCommandHandler : IRequestHandler<GetUserIdForStudentCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserIdForStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(GetUserIdForStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.Exists(x => x.Id == request.StudentId);
            if (studentExists)
            {
                var student = await unitOfWork.StudentRepository.GetStudent(request.StudentId);
                return student.User.Id;
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
