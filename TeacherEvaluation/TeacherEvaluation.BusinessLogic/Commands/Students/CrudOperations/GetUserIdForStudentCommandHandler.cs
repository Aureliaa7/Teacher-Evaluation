using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetUserIdForStudentCommandHandler : IRequestHandler<GetUserIdForStudentCommand, Guid>
    {
        private readonly IStudentRepository studentRepository;

        public GetUserIdForStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Guid> Handle(GetUserIdForStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.StudentId);
            if (studentExists)
            {
                var student = await studentRepository.GetStudent(request.StudentId);
                return student.User.Id;
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
