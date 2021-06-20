using MediatR;
using System.Collections.Generic;
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
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if (userExists)
            {
                var teacher = await unitOfWork.TeacherRepository.GetByUserIdAsync(request.UserId);
                return await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteriaAsync(ts => ts.Teacher.Id == teacher.Id &&
                                                                                            ts.Type == request.Type);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
