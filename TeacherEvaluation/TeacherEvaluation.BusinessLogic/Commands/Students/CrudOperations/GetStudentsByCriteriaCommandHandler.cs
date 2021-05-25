using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsByCriteriaCommandHandler : IRequestHandler<GetStudentsByCriteriaCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudentsByCriteriaCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool studyDomainExists = await unitOfWork.StudyDomainRepository.ExistsAsync(x => x.Id == request.StudyDomainId);
            bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
            if(studyDomainExists && specializationExists)
            {
                return await unitOfWork.StudentRepository.GetByCriteriaWithRelatedEntitiesAsync(request.StudyProgramme, 
                    request.StudyDomainId, request.SpecializationId, request.StudyYear);
            }
            throw new ItemNotFoundException("The study domain or the specialization was not found...");
        }
    }
}
