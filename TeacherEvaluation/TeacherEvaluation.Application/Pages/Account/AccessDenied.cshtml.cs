using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace TeacherEvaluation.Application.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public string RedirectPage { get; set; }

        public void OnGet()
        {
            string role = User.FindFirstValue(ClaimTypes.Role);
            RedirectPage = $"../MyProfile/{role}";
        }
    }
}