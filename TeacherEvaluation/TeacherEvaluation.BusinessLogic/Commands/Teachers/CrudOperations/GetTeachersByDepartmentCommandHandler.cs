using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeachersByDepartmentCommandHandler : IRequestHandler<GetTeachersByDepartmentCommand, IEnumerable<Teacher>>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetTeachersByDepartmentCommandHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetTeachersByDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await teacherRepository.GetByDepartment(request.Department);
        }
    }
}
