using Microsoft.AspNetCore.Identity;
using System;

namespace TeacherEvaluation.Domain.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string PIN { get; set; }
    }
}
