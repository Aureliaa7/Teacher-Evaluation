﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsBySemesterCommandHandler : IRequestHandler<GetSubjectsForEnrollmentsBySemesterCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForEnrollmentsBySemesterCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetSubjectsForEnrollmentsBySemesterCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                var allEnrollments = await unitOfWork.EnrollmentRepository.GetForStudentAsync(student.Id);

                var filteredEnrollments = allEnrollments.Where(e => e.TaughtSubject.Subject.StudyYear == student.StudyYear &&
                                                    e.TaughtSubject.Subject.Semester == request.Semester)
                                                    .GroupBy(e => e.TaughtSubject.Subject.Name)
                                                    .Select(e => e.First())
                                                    .ToList();

                return filteredEnrollments.Select(x => x.TaughtSubject.Subject).ToList();
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
