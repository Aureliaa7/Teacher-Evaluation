using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectsByTypeCommandHandler : IRequestHandler<GetTaughtSubjectsByTypeCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTaughtSubjectsByTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetTaughtSubjectsByTypeCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);
            if (userExists)
            {
                var allTeachers = await unitOfWork.TeacherRepository.GetAllWithRelatedEntities();
                var teacher = allTeachers.Where(x => x.User.Id == request.UserId).First();
                var allTaughtSubjects = await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntities();
                return allTaughtSubjects.Where(x => x.Teacher.Id == teacher.Id && x.Type == request.Type);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
