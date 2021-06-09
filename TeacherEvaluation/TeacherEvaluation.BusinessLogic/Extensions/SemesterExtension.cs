using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Extensions
{
    public static class SemesterExtension
    {
        //TODO double check this one
        public static Semester GetSemesterByDate(DateTime date)
        {
            return date.Month >= 6 && date.Day > 1 ? Semester.Fall : Semester.Spring;
        }
    }
}
