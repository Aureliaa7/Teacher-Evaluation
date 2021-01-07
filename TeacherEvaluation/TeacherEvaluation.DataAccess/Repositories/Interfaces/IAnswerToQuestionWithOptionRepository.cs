﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IAnswerToQuestionWithOptionRepository : IRepository<AnswerToQuestionWithOption> 
    {
        Task<IEnumerable<AnswerToQuestionWithOption>> GetByEnrollmentAndFormId(Guid enrollmentId, Guid formId);
    }
}
