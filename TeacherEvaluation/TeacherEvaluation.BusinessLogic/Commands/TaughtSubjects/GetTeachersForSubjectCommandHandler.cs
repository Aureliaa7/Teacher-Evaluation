using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class GetTeachersForSubjectCommandHandler : IRequestHandler<GetTeachersForSubjectCommand, IEnumerable<Teacher>>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;

        public GetTeachersForSubjectCommandHandler(ITaughtSubjectRepository taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetTeachersForSubjectCommand request, CancellationToken cancellationToken)
        {
            var allTeachers = await taughtSubjectRepository.GetAllWithRelatedEntities();
            return allTeachers.Where(x => x.Subject.Id == request.SubjectId && x.Type == request.Type).Select(x => x.Teacher).ToList();
        }
    }
}
