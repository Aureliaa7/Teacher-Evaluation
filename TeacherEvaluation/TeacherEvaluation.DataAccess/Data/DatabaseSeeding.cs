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
                    NormalizedEmail = "ALEXPOP@GMAIL.COM"
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
                    NormalizedEmail = "SILVIUNEGREA@GMAIL.COM"
                };
                IdentityResult resultDean = userManager.CreateAsync(dean, "Dean_ucv98@").Result;
                if (resultDean.Succeeded)
                {
                    userManager.AddToRoleAsync(dean, "Dean").Wait();
                }
            }
        }
    }
}

