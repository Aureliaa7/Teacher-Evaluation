using System;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class GradeVm
    {
        public Guid GradeId { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }
        [Display(Name = "Student")]
        public string StudentName { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Type")]
        public TaughtSubjectType Type { get; set; }
        public string Specialization { get; set; }
        public string Domain { get; set; }
        [Display(Name = "Programme")]
        public StudyProgramme StudyProgramme { get; set; }
        [Display(Name = "Study year")]
        public int StudyYear { get; set; }
        public string Group { get; set; }
    }
}
