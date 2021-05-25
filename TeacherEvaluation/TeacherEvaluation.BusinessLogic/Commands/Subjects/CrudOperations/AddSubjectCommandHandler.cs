using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class AddSubjectCommandHandler : AsyncRequestHandler<AddSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
            if (specializationExists) {
                var specialization = await unitOfWork.SpecializationRepository.GetSpecialization(request.SpecializationId);
                Subject newSubject = new Subject
                {
                    Name = request.Name,
                    NumberOfCredits = request.NumberOfCredits,
                    Specialization = specialization,
                    StudyYear = request.StudyYear
                };
                await unitOfWork.SubjectRepository.AddAsync(newSubject);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The Specialization was not found...");
            }
        }
    }
}
