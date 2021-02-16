﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITaughtSubjectRepository : IRepository<TaughtSubject>
    {
        Task<TaughtSubject> GetTaughtSubject(Guid id);
        Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntities();
        Task<TaughtSubject> GetTaughtSubject(Guid teacherId, Guid subjectId, TaughtSubjectType type);
        Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByCriteria(Department department, TaughtSubjectType taughtSubjectType);
        Task<IEnumerable<TaughtSubject>> GetByTeacherAndType(Guid teacherId, TaughtSubjectType type);
    }
}
