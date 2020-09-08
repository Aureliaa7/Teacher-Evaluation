using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommand : IRequest<List<string>>
    {
        public string PIN { get; set; }
        public int StudyYear { get; set; }
        public string Group { get; set; }
        public Guid SpecializationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationUrlTemplate { get; set; }
    }
}
