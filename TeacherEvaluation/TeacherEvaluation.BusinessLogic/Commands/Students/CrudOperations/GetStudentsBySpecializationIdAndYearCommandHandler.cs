using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsBySpecializationIdAndYearCommandHandler : IRequestHandler<GetStudentsBySpecializationIdAndYearCommand, IEnumerable<Student>>
    {
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<Specialization> specializationRepository;

        public GetStudentsBySpecializationIdAndYearCommandHandler(IStudentRepository studentRepository, IRepository<Specialization> specializationRepository)
        {
            this.studentRepository = studentRepository;
            this.specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsBySpecializationIdAndYearCommand request, CancellationToken cancellationToken)
        {
            bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
            if(specializationExists)
            {
                var students = await studentRepository.GetAllWithRelatedEntities();
                return students.Where(x => x.Specialization.Id == request.SpecializationId && 
                                           x.StudyYear == request.StudyYear);
            }
            throw new ItemNotFoundException("The specialization does not exist...");
        }
    }
}
