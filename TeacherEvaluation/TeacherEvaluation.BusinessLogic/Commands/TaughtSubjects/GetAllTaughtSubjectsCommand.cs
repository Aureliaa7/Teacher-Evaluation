﻿using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class GetAllTaughtSubjectsCommand : IRequest<IEnumerable<TaughtSubject>>
    {
    }
}
