using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students
{
    public class GetStudentByIdCommandHandler : IRequestHandler<GetStudentByIdCommand, Student>
    {
        private readonly IStudentRepository studentRepository;

        public GetStudentByIdCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Student> Handle(GetStudentByIdCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.Id);
            if (studentExists)
            {
                return await studentRepository.GetStudent(request.Id);
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
