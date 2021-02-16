using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.DTOs
{
    public class FormDto
    {
        public Form Form { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
