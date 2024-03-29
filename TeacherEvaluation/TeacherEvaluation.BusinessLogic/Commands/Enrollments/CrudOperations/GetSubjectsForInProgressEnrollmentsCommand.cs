﻿using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForInProgressEnrollmentsCommand : IRequest<IEnumerable<Subject>>
    {
        public Guid UserId { get; set; }
    }
}
