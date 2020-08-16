using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Helpers
{
    public class LoginResult
    {
        public List<string> ErrorMessages { get; set; }
        public string UserRole { get; set; }
    }
}
