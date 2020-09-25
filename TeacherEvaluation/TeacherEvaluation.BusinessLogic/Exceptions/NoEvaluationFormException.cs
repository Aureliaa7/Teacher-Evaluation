using System;

namespace TeacherEvaluation.BusinessLogic.Exceptions
{
    public class NoEvaluationFormException : Exception
    {
        public NoEvaluationFormException() : base() { }
        public NoEvaluationFormException(string message) : base(message) { }
        public NoEvaluationFormException(string message, Exception inner) : base(message, inner) { }
    }
}
