using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    class SearchGradesByCriteriaCommandHandler : IRequestHandler<SearchGradesByCriteriaCommand, IEnumerable<GradeVm>>
    {
        private readonly IUnitOfWork unitOfWork;

        public SearchGradesByCriteriaCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GradeVm>> Handle(SearchGradesByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);
            if(!teacherExists)
            {
                throw new ItemNotFoundException("The teacher was not found!");
            }
            
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(s => s.Id == request.SubjectId);
            if(!subjectExists)
            {
                throw new ItemNotFoundException("The subject was not found!");
            }
            
            var taughtSubject = (await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteriaAsync(
                ts => ts.Subject.Id == request.SubjectId && ts.Teacher.Id == request.TeacherId &&
                ts.Type == request.Type)).FirstOrDefault();
            if(taughtSubject == null)
            {
                throw new ItemNotFoundException("The taught subject was not found!");
            }

            var enrollments = await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubjectAsync(taughtSubject.Id);
            var filteredEnrollments = enrollments.Where(e => e.Grade.Value != null && e.Grade.Date != null &&
                        e.Grade.Date.Value.Year == request.FromYear);

            var gradeVms = new List<GradeVm>();
            foreach(var enrollment in filteredEnrollments)
            {
                gradeVms.Add(new GradeVm
                {
                    GradeId = enrollment.Grade.Id,
                    Date = (DateTime)enrollment.Grade.Date,
                    Domain = enrollment.Student.Specialization.StudyDomain.Name,
                    Specialization = enrollment.Student.Specialization.Name,
                    Grade = (int)enrollment.Grade.Value,
                    StudentName = string.Concat(enrollment.Student.User.FirstName, " ",
                    enrollment.Student.User.FathersInitial, " ", enrollment.Student.User.LastName),
                    StudyProgramme = enrollment.Student.Specialization.StudyDomain.StudyProgramme,
                    Subject = enrollment.TaughtSubject.Subject.Name,
                    TeacherName = string.Concat(enrollment.TaughtSubject.Teacher.User.FirstName, " ",
                    enrollment.TaughtSubject.Teacher.User.FathersInitial, " ", enrollment.TaughtSubject.Teacher.User.LastName),
                    Type = enrollment.Grade.Type,
                    Group = enrollment.Student.Group,
                    StudyYear = enrollment.Student.StudyYear
                });
            }
            return gradeVms;
        }
    }
}
