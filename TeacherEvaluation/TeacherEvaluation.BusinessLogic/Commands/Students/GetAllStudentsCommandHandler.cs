using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students
{
    public class GetAllStudentsCommandHandler : IRequestHandler<GetAllStudentsCommand, IEnumerable<Student>>
    {
        private readonly IStudentRepository studentRepository;

        public GetAllStudentsCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetAllStudentsCommand request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetAllWithRelatedEntities();
        }
    }
}
