using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class LoginResult
    {
        public List<string> ErrorMessages { get; set; }
        public string UserRole { get; set; }
    }
}
