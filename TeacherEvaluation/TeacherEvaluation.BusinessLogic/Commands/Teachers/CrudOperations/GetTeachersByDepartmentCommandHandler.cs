using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeachersByDepartmentCommandHandler : IRequestHandler<GetTeachersByDepartmentCommand, IEnumerable<Teacher>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeachersByDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetTeachersByDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TeacherRepository.GetByDepartment(request.Department);
        }
    }
}
