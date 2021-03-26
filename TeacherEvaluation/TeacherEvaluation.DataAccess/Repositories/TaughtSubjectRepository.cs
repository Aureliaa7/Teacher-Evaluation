﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class TaughtSubjectRepository : Repository<TaughtSubject>, ITaughtSubjectRepository
    {
        public TaughtSubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TaughtSubject> GetTaughtSubject(Guid id)
        {
            return await Context.Set<TaughtSubject>()
                .Where(x => x.Id == id)
                .Include(entity => entity.Teacher)
                    .ThenInclude(teacher => teacher.User)
                .Include(entity => entity.Subject)
                .FirstAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntities()
        {
            return await Context.Set<TaughtSubject>()
                .Include(entity => entity.Teacher)
                    .ThenInclude(user => user.User)
                .Include(entity => entity.Subject)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaughtSubject> GetTaughtSubject(Guid teacherId, Guid subjectId, TaughtSubjectType type)
        {
            return await Context.Set<TaughtSubject>()
              .Where(x => x.Teacher.Id == teacherId && x.Subject.Id == subjectId && x.Type == type)
              .Include(entity => entity.Teacher)
                  .ThenInclude(teacher => teacher.User)
              .Include(entity => entity.Subject)
              .FirstAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByDepartmentAndType(Department department, TaughtSubjectType taughtSubjectType)
        {
            return await Context.Set<TaughtSubject>()
                .Where(x => x.Teacher.Department == department && x.Type == taughtSubjectType)
                .Include(entity => entity.Teacher)
                    .ThenInclude(teacher => teacher.User)
                .Include(entity => entity.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByTeacherIdAndType(Guid teacherId, TaughtSubjectType type)
        {
            return await Context.Set<TaughtSubject>()
               .Where(x => x.Teacher.Id == teacherId && x.Type == type)
               .Include(entity => entity.Teacher)
                   .ThenInclude(teacher => teacher.User)
               .Include(entity => entity.Subject)
               .ToListAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsBySubjectIdAndType(Guid subjectId, TaughtSubjectType type)
        {
            return await Context.Set<TaughtSubject>()
               .Where(x => x.Subject.Id == subjectId && x.Type == type)
               .Include(entity => entity.Teacher)
                   .ThenInclude(teacher => teacher.User)
               .Include(entity => entity.Subject)
               .ToListAsync();
        }
    }
}
