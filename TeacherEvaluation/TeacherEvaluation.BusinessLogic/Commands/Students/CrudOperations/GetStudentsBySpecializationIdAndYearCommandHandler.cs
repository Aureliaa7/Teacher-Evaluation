using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsBySpecializationIdAndYearCommandHandler : IRequestHandler<GetStudentsBySpecializationIdAndYearCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudentsBySpecializationIdAndYearCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsBySpecializationIdAndYearCommand request, CancellationToken cancellationToken)
        {
            bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
            if(specializationExists)
            {
                var students = await unitOfWork.StudentRepository.GetAllWithRelatedEntitiesAsync();
                return students.Where(x => x.Specialization.Id == request.SpecializationId && 
                                           x.StudyYear == request.StudyYear);
            }
            throw new ItemNotFoundException("The specialization does not exist...");
        }
    }
}
