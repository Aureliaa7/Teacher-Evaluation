using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsByCriteriaCommandHandler : IRequestHandler<GetStudentsByCriteriaCommand, IEnumerable<Student>>
    {
        private readonly IRepository<StudyDomain> studyDomainRepository;
        private readonly IRepository<Specialization> specializationRepository;
        private readonly IStudentRepository studentRepository;

        public GetStudentsByCriteriaCommandHandler(IRepository<StudyDomain> studyDomainRepository, 
            IRepository<Specialization> specializationRepository, IStudentRepository studentRepository)
        {
            this.studyDomainRepository = studyDomainRepository;
            this.specializationRepository = specializationRepository;
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool studyDomainExists = await studyDomainRepository.Exists(x => x.Id == request.StudyDomainId);
            bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
            if(studyDomainExists && specializationExists)
            {
                return await studentRepository.GetByCriteriaWithRelatedEntities(request.StudyProgramme, 
                    request.StudyDomainId, request.SpecializationId, request.StudyYear);
            }
            throw new ItemNotFoundException("The study domain or the specialization was not found...");
        }
    }
}
