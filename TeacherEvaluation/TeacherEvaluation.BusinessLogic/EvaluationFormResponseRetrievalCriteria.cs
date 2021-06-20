using System;

namespace TeacherEvaluation.BusinessLogic
{
    public class EvaluationFormResponseRetrievalCriteria
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
