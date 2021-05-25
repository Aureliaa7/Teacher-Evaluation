using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class TeacherAccountDetailsCommandHandler : IRequestHandler<TeacherAccountDetailsCommand, TeacherAccountDetailsVm>
    {
        private readonly IUnitOfWork unitOfWork;

        public TeacherAccountDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TeacherAccountDetailsVm> Handle(TeacherAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(u => u.Id == request.UserId);
            if(userExists)
            {
                var teacher = await unitOfWork.TeacherRepository.GetByUserIdAsync(request.UserId);
                return new TeacherAccountDetailsVm
                {
                    FirstName = teacher.User.FirstName,
                    LastName = teacher.User.LastName,
                    FathersInitial = teacher.User.FathersInitial,
                    Email = teacher.User.Email,
                    PIN = teacher.User.PIN,
                    Degree = teacher.Degree,
                    Department = teacher.Department.ToString(),
                    Role = "Teacher"
                };
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
