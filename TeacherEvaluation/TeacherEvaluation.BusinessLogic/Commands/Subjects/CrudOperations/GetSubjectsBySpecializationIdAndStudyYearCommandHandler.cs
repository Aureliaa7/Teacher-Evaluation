using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class GetSubjectsBySpecializationIdAndStudyYearCommandHandler : IRequestHandler<GetSubjectsBySpecializationIdAndStudyYearCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsBySpecializationIdAndStudyYearCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetSubjectsBySpecializationIdAndStudyYearCommand request, CancellationToken cancellationToken)
        {
            bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
            if (specializationExists)
            {
                var subjects = await unitOfWork.SubjectRepository.GetSubjectsByCriteria(request.SpecializationId, request.StudyYear);
                return subjects;
            }
            throw new ItemNotFoundException("The specialization was not found");
        }
    }
}
