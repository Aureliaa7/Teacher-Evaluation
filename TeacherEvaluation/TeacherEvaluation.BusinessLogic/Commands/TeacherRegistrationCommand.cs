using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class TeacherRegistrationCommand : IRequest<List<string>>
    {
        public string PIN { get; set; }
        public string Degree { get; set; }
        public Department Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
