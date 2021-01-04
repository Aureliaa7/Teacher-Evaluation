using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class GetAllSubjectsCommandHandler : IRequestHandler<GetAllSubjectsCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllSubjectsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetAllSubjectsCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.SubjectRepository.GetAll();
        }
    }
}
