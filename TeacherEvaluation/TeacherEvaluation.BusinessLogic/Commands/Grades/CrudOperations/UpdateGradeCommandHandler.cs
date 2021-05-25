﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class UpdateGradeCommandHandler : AsyncRequestHandler<UpdateGradeCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateGradeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected async override Task Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.ExistsAsync(x => x.Id == request.StudentId);
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.SubjectId);
            if (studentExists && subjectExists)
            {
                IEnumerable<Enrollment> enrollments = await unitOfWork.EnrollmentRepository.GetForStudent(request.StudentId);
                Enrollment enrollment = enrollments.Where(x => x.TaughtSubject.Subject.Id == request.SubjectId && x.TaughtSubject.Type == request.Type).First();
                enrollment.Grade.Value = request.Value;
                enrollment.Grade.Date = request.Date;
                enrollment.State = EnrollmentState.Done;
                unitOfWork.EnrollmentRepository.Update(enrollment);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The student or the subject was not found...");
            }
        }
    }
}
