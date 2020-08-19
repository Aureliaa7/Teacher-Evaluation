using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class AssignedSubjectVerificationCommandHandler : IRequestHandler<AssignedSubjectVerificationCommand, bool>
    {
        private readonly IRepository<TaughtSubject> taughtSubjectRepository;

        public AssignedSubjectVerificationCommandHandler(IRepository<TaughtSubject> taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<bool> Handle(AssignedSubjectVerificationCommand request, CancellationToken cancellationToken)
        {
            return await taughtSubjectRepository.Exists(x => x.Teacher.Id == request.TeacherId &&
                                                             x.Subject.Id == request.SubjectId &&
                                                             x.Type == request.Type);
        }
    }
}
