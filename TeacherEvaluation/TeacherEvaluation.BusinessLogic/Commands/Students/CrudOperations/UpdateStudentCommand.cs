using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class UpdateStudentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PIN { get; set; }
        public int StudyYear { get; set; }
        public string Section { get; set; }
        public string Group { get; set; }
        public StudyProgramme StudyProgramme { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
    }
}
