using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class GetAllStudentsCommandHandler : IRequestHandler<GetAllStudentsCommand, IEnumerable<Student>>
    {
        private readonly IRepository<Student> studentRepository;

        public GetAllStudentsCommandHandler(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetAllStudentsCommand request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetAll();
        }
    }
}
