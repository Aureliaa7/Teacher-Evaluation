using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class DeleteEnrollmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
