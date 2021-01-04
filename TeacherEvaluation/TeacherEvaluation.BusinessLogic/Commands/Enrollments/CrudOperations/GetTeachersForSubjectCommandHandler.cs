using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetTeachersForSubjectCommandHandler : IRequestHandler<GetTeachersForSubjectCommand, IEnumerable<Teacher>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeachersForSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetTeachersForSubjectCommand request, CancellationToken cancellationToken)
        {
            var allTeachers = await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntities();
            return allTeachers.Where(x => x.Subject.Id == request.SubjectId && x.Type == request.Type).Select(x => x.Teacher).ToList();
        }
    }
}
