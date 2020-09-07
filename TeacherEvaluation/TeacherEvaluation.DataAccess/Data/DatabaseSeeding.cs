using Microsoft.AspNetCore.Identity;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.Data
{
    public class DatabaseSeeding
    {
        public static void AddDeanAndAdministrator(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("alexpop@gmail.com").Result == null)
            {
                ApplicationUser administrator = new ApplicationUser
                {
                    Email = "alexpop@gmail.com",
                    FirstName = "Alexandru",
                    LastName = "Pop",
                    FathersInitial = "R.",
                    UserName = "alexpop@gmail.com",
                    NormalizedUserName = "ALEXPOP@GMAIL.COM",
                    NormalizedEmail = "ALEXPOP@GMAIL.COM",
                    EmailConfirmed = true
                };
                IdentityResult resultAdministrator = userManager.CreateAsync(administrator, "Admin_ucv98@").Result;
                if (resultAdministrator.Succeeded)
                {
                    userManager.AddToRoleAsync(administrator, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("silviunegrea@gmail.com").Result == null)
            {
                ApplicationUser dean = new ApplicationUser
                {
                    Email = "silviunegrea@gmail.com",
                    FirstName = "Silviu",
                    LastName = "Negrea",
                    FathersInitial = "M.",
                    UserName = "silviunegrea@gmail.com",
                    NormalizedUserName = "SILVIUNEGREA@GMAIL.COM",
                    NormalizedEmail = "SILVIUNEGREA@GMAIL.COM",
                    EmailConfirmed = true
                };
                IdentityResult resultDean = userManager.CreateAsync(dean, "Dean_ucv98@").Result;
                if (resultDean.Succeeded)
                {
                    userManager.AddToRoleAsync(dean, "Dean").Wait();
                }
            }
        }

        public static void AddRoles(RoleManager<ApplicationRole> roleManager)
        {
            if(roleManager.FindByNameAsync("Administrator").Result == null)
            {
                ApplicationRole administratorRole = new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                };
                IdentityResult resultAdmin = roleManager.CreateAsync(administratorRole).Result;
            }

            if (roleManager.FindByNameAsync("Dean").Result == null)
            {
                ApplicationRole deanRole = new ApplicationRole
                {
                    Name = "Dean",
                    NormalizedName = "DEAN"
                };
                IdentityResult resultDean = roleManager.CreateAsync(deanRole).Result;
            }

            if (roleManager.FindByNameAsync("Student").Result == null)
            {
                ApplicationRole studentRole = new ApplicationRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                };
                IdentityResult resultStudent = roleManager.CreateAsync(studentRole).Result;
            }

            if (roleManager.FindByNameAsync("Teacher").Result == null)
            {
                ApplicationRole teacherRole = new ApplicationRole
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                };
                IdentityResult resultTeacher = roleManager.CreateAsync(teacherRole).Result;
            }
        }
    }
}

