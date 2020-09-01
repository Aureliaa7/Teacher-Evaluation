using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommand : IRequest<List<string>>
    {
        public string PIN { get; set; }
        public int StudyYear { get; set; }
        public string Section { get; set; }
        public string Group { get; set; }
        public StudyProgramme StudyProgramme { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationUrlTemplate { get; set; }
    }
}
