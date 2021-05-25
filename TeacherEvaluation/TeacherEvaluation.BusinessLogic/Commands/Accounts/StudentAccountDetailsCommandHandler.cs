using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class StudentAccountDetailsCommandHandler : IRequestHandler<StudentAccountDetailsCommand, StudentAccountDetailsVm>
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentAccountDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<StudentAccountDetailsVm> Handle(StudentAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(u => u.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                return new StudentAccountDetailsVm
                {
                    FirstName = student.User.FirstName,
                    LastName = student.User.LastName,
                    Email = student.User.Email,
                    FathersInitial = student.User.FathersInitial,
                    PIN = student.User.PIN,
                    Role = "Student",
                    Group = student.Group,
                    Specialization = student.Specialization.Name,
                    StudyDomain = student.Specialization.StudyDomain.Name,
                    StudyYear = student.StudyYear,
                    StudyProgramme = student.Specialization.StudyDomain.StudyProgramme.ToString()
                };
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
