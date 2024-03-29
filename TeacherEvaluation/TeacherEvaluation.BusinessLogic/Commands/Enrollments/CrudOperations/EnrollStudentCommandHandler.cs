﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class EnrollStudentCommandHandler : AsyncRequestHandler<EnrollStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public EnrollStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(e => e.Student.Id == request.StudentId && e.TaughtSubject.Teacher.Id == request.TeacherId
            && e.TaughtSubject.Subject.Id == request.SubjectId);
            if (enrollmentExists)
            {
                throw new Exception("The enrollment already exists!");
            }

            bool teacherExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Teacher.Id == request.TeacherId);
            bool subjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Subject.Id == request.SubjectId);
            bool studentExists = await unitOfWork.StudentRepository.ExistsAsync(x => x.Id == request.StudentId);

            if (teacherExists && subjectExists && studentExists)
            {
                TaughtSubject taughtSubject = await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectAsync(request.TeacherId, request.SubjectId, request.Type);
                Student student = await unitOfWork.StudentRepository.GetStudentAsync(request.StudentId);
                Grade grade = new Grade
                {
                    Value = null,
                    Date = null,
                    Type = request.Type
                };

                Enrollment newEnrollment = new Enrollment
                {
                    Student = student,
                    TaughtSubject = taughtSubject,
                    Grade = grade,
                    State = EnrollmentState.InProgress,
                    NumberOfAttendances = 0,  
                    Semester = taughtSubject.Subject.Semester
                };
                await unitOfWork.EnrollmentRepository.AddAsync(newEnrollment);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The teacher or the subject was not found...");
            }
        }
    }
}
