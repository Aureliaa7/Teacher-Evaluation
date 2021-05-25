using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetAllStudentsCommandHandler : IRequestHandler<GetAllStudentsCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllStudentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetAllStudentsCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.StudentRepository.GetAllWithRelatedEntitiesAsync();
        }
    }
}
