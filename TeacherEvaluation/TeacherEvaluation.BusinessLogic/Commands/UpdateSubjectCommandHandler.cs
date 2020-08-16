using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class UpdateSubjectCommandHandler : AsyncRequestHandler<UpdateSubjectCommand>
    {
        private readonly IRepository<Subject> subjectRepository;

        public UpdateSubjectCommandHandler(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.Id);
            if (subjectExists)
            {
                Subject subjectToBeUpdated = await subjectRepository.Get(request.Id);
                subjectToBeUpdated.Name = request.Name;
                subjectToBeUpdated.NumberOfCredits = request.NumberOfCredits;
                await subjectRepository.Update(subjectToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The subject was not found...");
            }
        }
    }
}
