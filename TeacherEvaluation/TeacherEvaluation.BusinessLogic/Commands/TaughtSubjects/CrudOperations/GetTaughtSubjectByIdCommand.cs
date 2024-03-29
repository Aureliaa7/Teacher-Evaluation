﻿using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectByIdCommand : IRequest<TaughtSubject>
    {
        public Guid Id { get; set; }
    }
}
