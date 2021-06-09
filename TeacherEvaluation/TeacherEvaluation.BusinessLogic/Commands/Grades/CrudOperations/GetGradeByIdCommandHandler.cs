using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    class GetGradeByIdCommandHandler : IRequestHandler<GetGradeByIdCommand, GradeVm>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetGradeByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GradeVm> Handle(GetGradeByIdCommand request, CancellationToken cancellationToken)
        {
            bool gradeExists = await unitOfWork.GradeRepository.ExistsAsync(g => g.Id == request.Id);
            if (!gradeExists)
            {
                throw new ItemNotFoundException("The grade was not found!");
            }

            var enrollment = (await unitOfWork.EnrollmentRepository.GetAllWithRelatedEntitiesAsync())
                .FirstOrDefault(e => e.Grade.Id == request.Id);
            if(enrollment != null)
            {
                return new GradeVm
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
                };
            }
            throw new ItemNotFoundException("The enrollment was not found!");
        }
    }
}
