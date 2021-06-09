using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.Extensions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsBySemesterCommandHandler : IRequestHandler<GetSubjectsForEnrollmentsBySemesterCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForEnrollmentsBySemesterCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //Limitation: For the current implementation, the students should first evaluate their teachers and then the
        // admin can add the grades.When adding the grades, the enrollment state is marked as done, thus the students
        // can't evaluate their teachers

        //TODO take the enrollments by semester and current year, not enrollment state.
        // => Add EnrollmentDate and Semester to the Enrollments table(first create an enum for Semester).
        // These new values won't be introduced by admin, they will be set considering the current date 
        // => Remove EnrollmentState from Forms table since is no longer needed and add Semester field(which will be set
        // considering the current date; month > June => Fall(sem1) and month<June=> Spring(sem2))
        // => In GetSubjectsForEnrollmentsCommandHandler, select the enrollments by Semester(provided in Form) and current year
        // => The available form should be taken considering the current date(as it is currently)


        //TODO test it
        public async Task<IEnumerable<Subject>> Handle(GetSubjectsForEnrollmentsBySemesterCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                var allEnrollments = await unitOfWork.EnrollmentRepository.GetForStudentAsync(student.Id);
                var currentSemester = SemesterExtension.GetSemesterByDate(DateTime.UtcNow);
               // var enrollments = allEnrollments.Where(x => x.Semester == currentSemester);
                var currentYear = DateTime.UtcNow.Year;

                // considering the case where the Dean will create only one evaluation form/semester(both in the same year,
                // this means each of them would be created at the end of the semester)
                

                //TODO modify this filtering because right now does not return the correct result
                var enrollments = currentSemester == Semester.Fall ? 
                    allEnrollments.Where(x => currentYear == x.EnrollmentDate.Year + 1) :
                    allEnrollments.Where(x => currentYear == x.EnrollmentDate.Year);

                var a = enrollments.ToList();

                var subjectEnrollments = enrollments.GroupBy(x => x.TaughtSubject.Subject.Name).Select(x => x.First()).ToList();
                return subjectEnrollments.Select(x => x.TaughtSubject.Subject).ToList();
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
