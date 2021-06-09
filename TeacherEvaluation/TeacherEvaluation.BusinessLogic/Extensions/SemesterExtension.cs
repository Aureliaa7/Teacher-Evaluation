using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Extensions
{
    public static class SemesterExtension
    {
        public static Semester GetSemesterByDate(DateTime date)
        {
            return date.Month >= 6 && date.Day > 1 ? Semester.Fall : Semester.Spring;
        }
    }
}
