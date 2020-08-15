using MediatR;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class ChangePasswordCommand : IRequest<List<string>>
    {
        public string CurrentUserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
