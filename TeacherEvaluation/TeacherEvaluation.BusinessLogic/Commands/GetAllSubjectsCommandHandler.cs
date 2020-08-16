using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class GetAllSubjectsCommandHandler : IRequestHandler<GetAllSubjectsCommand, IEnumerable<Subject>>
    {
        private readonly IRepository<Subject> subjectRepository;

        public GetAllSubjectsCommandHandler(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> Handle(GetAllSubjectsCommand request, CancellationToken cancellationToken)
        {
            return await subjectRepository.GetAll();
        }
    }
}
