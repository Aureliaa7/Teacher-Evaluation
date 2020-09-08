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
    public class GetStudentsBySpecializationIdCommandHandler : IRequestHandler<GetStudentsBySpecializationIdCommand, IEnumerable<Student>>
    {
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<Specialization> specializationRepository;

        public GetStudentsBySpecializationIdCommandHandler(IStudentRepository studentRepository, IRepository<Specialization> specializationRepository)
        {
            this.studentRepository = studentRepository;
            this.specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsBySpecializationIdCommand request, CancellationToken cancellationToken)
        {
            bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
            if(specializationExists)
            {
                var students = await studentRepository.GetAllWithRelatedEntities();
                return students.Where(x => x.Specialization.Id == request.SpecializationId);
            }
            throw new ItemNotFoundException("The specialization does not exist...");
        }
    }
}
