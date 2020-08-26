using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class DeleteEnrollmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
