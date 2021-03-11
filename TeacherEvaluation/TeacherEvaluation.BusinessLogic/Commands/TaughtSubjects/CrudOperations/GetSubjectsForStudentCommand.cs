using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetSubjectsForStudentCommand : IRequest<IEnumerable<TakenSubjectVm>>
    {
        public Guid UserId { get; set; }
        public TaughtSubjectType SubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
    }
}
