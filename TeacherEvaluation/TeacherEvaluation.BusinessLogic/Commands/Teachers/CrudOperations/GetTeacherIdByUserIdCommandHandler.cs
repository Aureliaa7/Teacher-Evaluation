using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherIdByUserIdCommandHandler : IRequestHandler<GetTeacherIdByUserIdCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeacherIdByUserIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(GetTeacherIdByUserIdCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.User.Id == request.UserId); 
            if(teacherExists)
            {
                var teacher = await unitOfWork.TeacherRepository.GetByUserId(request.UserId);
                return teacher.Id;
            }
            throw new ItemNotFoundException("The teacher was not found");
        }
    }
}
