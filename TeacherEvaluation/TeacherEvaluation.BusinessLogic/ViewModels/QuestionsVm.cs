using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class QuestionsVm
    {
        public IEnumerable<Question> LikertQuestions { get; set; }
        public IEnumerable<Question> FreeFormQuestions { get; set; }
    }
}
