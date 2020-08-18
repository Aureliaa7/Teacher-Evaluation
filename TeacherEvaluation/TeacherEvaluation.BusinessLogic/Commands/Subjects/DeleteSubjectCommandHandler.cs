using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects
{
    public class DeleteSubjectCommandHandler : AsyncRequestHandler<DeleteSubjectCommand>
    {

        private readonly IRepository<Subject> subjectRepository;

        public DeleteSubjectCommandHandler(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.Id);
            if(subjectExists)
            {
                await subjectRepository.Remove(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The subject to be deleted was not found...");
            }
        }
    }
}
