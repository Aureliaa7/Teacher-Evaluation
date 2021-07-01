using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class UpdateSubjectCommandHandler : AsyncRequestHandler<UpdateSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.Id);
            if (subjectExists)
            {
                bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
                if (specializationExists)
                {
                    var specialization = await unitOfWork.SpecializationRepository.GetSpecializationAsync(request.SpecializationId);
                    Subject subjectToBeUpdated = await unitOfWork.SubjectRepository.GetAsync(request.Id);
                    subjectToBeUpdated.Name = request.Name;
                    subjectToBeUpdated.NumberOfCredits = request.NumberOfCredits;
                    subjectToBeUpdated.StudyYear = request.StudyYear;
                    subjectToBeUpdated.Specialization = specialization;
                    subjectToBeUpdated.Semester = request.Semester;
                    await unitOfWork.SubjectRepository.UpdateAsync(subjectToBeUpdated);
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new ItemNotFoundException("The specialization was not found...");
                }
            }
            else
            {
                throw new ItemNotFoundException("The subject was not found...");
            }
        }
    }
}
