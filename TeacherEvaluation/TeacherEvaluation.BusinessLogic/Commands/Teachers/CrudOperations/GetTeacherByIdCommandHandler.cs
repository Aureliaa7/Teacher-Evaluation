using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherByIdCommandHandler : IRequestHandler<GetTeacherByIdCommand, Teacher>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeacherByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Teacher> Handle(GetTeacherByIdCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                return await unitOfWork.TeacherRepository.GetTeacher(request.Id);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
