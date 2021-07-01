using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class LoginResult
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public string UserRole { get; set; }
    }
}
