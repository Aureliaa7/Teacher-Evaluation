using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class GetAllTeachersCommandHandler : IRequestHandler<GetAllTeachersCommand, IEnumerable<Teacher>>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetAllTeachersCommandHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetAllTeachersCommand request, CancellationToken cancellationToken)
        {
            return await teacherRepository.GetAllWithRelatedEntities();
        }
    }
}
