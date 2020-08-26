using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class TeacherRegistrationCommandHandler : IRequestHandler<TeacherRegistrationCommand, List<string>>
    {
        private readonly IRepository<Teacher> teacherRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public TeacherRegistrationCommandHandler(IRepository<Teacher> teacherRepository, UserManager<ApplicationUser> userManager)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
        }

        public async Task<List<string>> Handle(TeacherRegistrationCommand command, CancellationToken cancellationToken)
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
                await userManager.AddToRoleAsync(newApplicationUser, "Teacher");

                Teacher teacher = new Teacher
                {
                    Degree = command.Degree,
                    Department = command.Department,
                    PIN = command.PIN,
                    User = newApplicationUser
                };
                await teacherRepository.Add(teacher);
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
