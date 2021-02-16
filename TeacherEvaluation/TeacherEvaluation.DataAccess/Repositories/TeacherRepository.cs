﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Teacher> GetTeacher(Guid id)
        {
            return await Context.Set<Teacher>()
              .Where(x => x.Id == id)
              .Include(x => x.User)
              .FirstAsync();
        }

        public async Task<Teacher> GetByUserId(Guid userId)
        {
            return await Context.Set<Teacher>()
              .Where(x => x.User.Id == userId)
              .Include(x => x.User)
              .FirstAsync();
        }

        //TODO la fel ca la student
        public async Task Delete(Guid id)
        {
            var teacherToBeDeleted = await GetTeacher(id);
            var userToBeDeleted = teacherToBeDeleted.User;
            Context.Set<Teacher>().Remove(teacherToBeDeleted);
            Context.Set<ApplicationUser>().Remove(userToBeDeleted);

            //await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllWithRelatedEntities()
        {
            return await Context.Set<Teacher>()
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByDepartment(Department department)
        {
            return await Context.Set<Teacher>()
                .Where(x => x.Department == department)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
