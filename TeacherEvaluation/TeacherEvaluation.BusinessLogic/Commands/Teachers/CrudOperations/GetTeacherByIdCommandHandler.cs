using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherByIdCommandHandler : IRequestHandler<GetTeacherByIdCommand, Teacher>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetTeacherByIdCommandHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<Teacher> Handle(GetTeacherByIdCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await teacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                return await teacherRepository.GetTeacher(request.Id);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
