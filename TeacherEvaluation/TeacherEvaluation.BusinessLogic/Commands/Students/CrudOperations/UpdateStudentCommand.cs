﻿using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class UpdateStudentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PIN { get; set; }
        public int StudyYear { get; set; }
        public Guid SpecializationId { get; set; }
        public string Group { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersInitial { get; set; }
        public string Email { get; set; }
    }
}
