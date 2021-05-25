using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetAllTeachersCommandHandler : IRequestHandler<GetAllTeachersCommand, IEnumerable<Teacher>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllTeachersCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetAllTeachersCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TeacherRepository.GetAllWithRelatedEntitiesAsync();
        }
    }
}
