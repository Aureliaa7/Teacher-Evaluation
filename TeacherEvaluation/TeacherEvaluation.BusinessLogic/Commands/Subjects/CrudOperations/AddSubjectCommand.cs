using MediatR;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class AddSubjectCommand : IRequest
    {
        public string Name { get; set; }
        public int NumberOfCredits { get; set; }
    }
}
