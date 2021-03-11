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
    public class GetSubjectsForTeacherCommandHandler : IRequestHandler<GetSubjectsForTeacherCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForTeacherCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetSubjectsForTeacherCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(x => x.User.Id == request.UserId);
            if(userExists && teacherExists)
            {
                var teacher = (await unitOfWork.TeacherRepository.GetAllWithRelatedEntities()).Where(x => x.User.Id == request.UserId).First();
                var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntities();
                return taughtSubjects.Where(x => x.Teacher.Id == teacher.Id);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
