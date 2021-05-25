using System.ComponentModel.DataAnnotations;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class UserAccountDetailsVm
    {
        [Display(Name = "First Name")]
        public string FirstName { get; init; }
        [Display(Name = "Last Name")]
        public string LastName { get; init; }
        public string Email { get; init; }
        [Display(Name = "Initial")]
        public string FathersInitial { get; init; }
        [Display(Name = "CNP")]
        public string PIN { get; init; }
        public string Role { get; init; }
    }
}