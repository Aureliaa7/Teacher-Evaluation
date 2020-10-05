using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetEnrollmentsByCriteriaCommandHandler : IRequestHandler<GetEnrollmentsByCriteriaCommand, IEnumerable<Enrollment>>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<StudyDomain> studyDomainRepository;
        private readonly IRepository<Specialization> specializationRepository;

        public GetEnrollmentsByCriteriaCommandHandler(IEnrollmentRepository enrollmentRepository, IRepository<StudyDomain> studyDomainRepository,
            IRepository<Specialization> specializationRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.studyDomainRepository = studyDomainRepository;
            this.specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetEnrollmentsByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool studyDomainExists = await studyDomainRepository.Exists(x => x.Id == request.StudyDomainId);
            bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
            if(studyDomainExists && specializationExists)
            {
                var allEnrollments = await enrollmentRepository.GetAllWithRelatedEntities();
                var enrollments = new List<Enrollment>();
                enrollments = allEnrollments.Where(x => (x.Student.Specialization.StudyDomain.StudyProgramme == request.StudyProgramme) &&
                                                                    (x.Student.Specialization.StudyDomain.Id == request.StudyDomainId) &&
                                                                    (x.Student.Specialization.Id == request.SpecializationId) &&
                                                                    (x.TaughtSubject.Type == request.TaughtSubjectType) &&
                                                                    (x.State == request.EnrollmentState) &&
                                                                    (x.Student.StudyYear == request.StudyYear)).ToList();
                return enrollments;
            }
            throw new ItemNotFoundException("The study domain or the specialization was not found...");
        }
    }
}
