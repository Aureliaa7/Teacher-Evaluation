using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class GetSubjectByIdCommandHandler : IRequestHandler<GetSubjectByIdCommand, Subject>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Subject> Handle(GetSubjectByIdCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.Id);
            if (subjectExists)
            {
                return await unitOfWork.SubjectRepository.GetWithRelatedEntities(request.Id);
            }
            throw new ItemNotFoundException("The subject was not found...");
        }
    }
}
