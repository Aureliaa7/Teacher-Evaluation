using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Students
{
    public class StudentRegistrationCommandHandler : IRequestHandler<StudentRegistrationCommand, List<string>>
    {
        private readonly IRepository<Student> studentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public StudentRegistrationCommandHandler(IRepository<Student> studentRepository, UserManager<ApplicationUser> userManager)
        {
            this.studentRepository = studentRepository;
            this.userManager = userManager;
        }

        public async Task<List<string>> Handle(StudentRegistrationCommand command, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            ApplicationUser newApplicationUser = new ApplicationUser
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                FathersInitial = command.FathersInitial,
                Email = command.Email,
                UserName = command.Email
            };

            var result = await userManager.CreateAsync(newApplicationUser, command.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newApplicationUser, "Student");

                Student student = new Student
                {
                    StudyProgramme = command.StudyProgramme,
                    Group = command.Group,
                    Section = command.Section,
                    StudyYear = command.StudyYear,
                    PIN = command.PIN,
                    User = newApplicationUser
                };
                await studentRepository.Add(student);
                errorMessages = null;
            }
            else
            {
                foreach (var errorMessage in result.Errors)
                {
                    errorMessages.Add(errorMessage.Description);
                }
            }
            return errorMessages;
        }
    }
}
