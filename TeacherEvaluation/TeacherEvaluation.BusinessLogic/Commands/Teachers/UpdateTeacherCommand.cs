using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class UpdateTeacherCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PIN { get; set; }
        public string Degree { get; set; }
        public Department Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
    }
}
