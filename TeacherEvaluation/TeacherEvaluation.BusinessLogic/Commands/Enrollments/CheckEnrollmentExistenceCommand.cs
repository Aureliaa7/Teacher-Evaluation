﻿using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class CheckEnrollmentExistenceCommand : IRequest<bool>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
