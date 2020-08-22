using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherEvaluation.Application.Pages.Errors
{
    public class NotFoundPageModel : PageModel
    {
        public string RedirectPage { get; set; }

        public void OnGet()
        {
            if (User.IsInRole("Administrator"))
            {
                RedirectPage = "../Dashboards/Admin";
            }
            else if (User.IsInRole("Dean"))
            {
                RedirectPage = "../Dashboards/Dean";
            }
            else if (User.IsInRole("Student"))
            {
                RedirectPage = "../Dashboards/Student";
            }
            else if (User.IsInRole("Teacher"))
            {
                RedirectPage = "../Dashboards/Teacher";
            }
            else
            {
                RedirectPage = "../Account/Login";
            }
        }
    }
}