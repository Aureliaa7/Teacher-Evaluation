﻿using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades
{
    public class UpdateGradeCommand : IRequest
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
