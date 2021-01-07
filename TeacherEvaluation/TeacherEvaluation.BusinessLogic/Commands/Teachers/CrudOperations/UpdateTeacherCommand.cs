using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
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
