using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class AddSubjectCommandHandler : AsyncRequestHandler<AddSubjectCommand>
    {
        private readonly IRepository<Subject> subjectRepository;

        public AddSubjectCommandHandler(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        protected override async Task Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject newSubject = new Subject
            {
                Name = request.Name,
                NumberOfCredits = request.NumberOfCredits
            };
            await subjectRepository.Add(newSubject);
        }
    }
}
