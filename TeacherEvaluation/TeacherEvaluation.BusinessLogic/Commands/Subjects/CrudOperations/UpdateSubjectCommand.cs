﻿using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class UpdateSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfCredits { get; set; }
        public Guid SpecializationId { get; set; }
        public int StudyYear { get; set; }
    }
}
