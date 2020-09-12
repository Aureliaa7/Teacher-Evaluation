using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class UpdateGradeCommandHandler : AsyncRequestHandler<UpdateGradeCommand>
    {
        private readonly IRepository<Grade> gradeRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Subject> subjectRepository;

        public UpdateGradeCommandHandler(IRepository<Grade> gradeRepository, IEnrollmentRepository enrollmentRepository,
            IRepository<Student> studentRepository, IRepository<Subject> subjectRepository)
        {
            this.gradeRepository = gradeRepository;
            this.enrollmentRepository = enrollmentRepository;
            this.studentRepository = studentRepository;
            this.subjectRepository = subjectRepository;
        }

        protected async override Task Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.StudentId);
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);
            if (studentExists && subjectExists)
            {
                IEnumerable<Enrollment> enrollments = await enrollmentRepository.GetForStudent(request.StudentId);
                Enrollment enrollment = enrollments.Where(x => x.TaughtSubject.Subject.Id == request.SubjectId && x.TaughtSubject.Type == request.Type).First();
                //Grade gradeToBeUpdated = enrollment.Grade;
                //gradeToBeUpdated.Value = request.Value;
                //gradeToBeUpdated.Date = request.Date;

                enrollment.Grade.Value = request.Value;
                enrollment.Grade.Date = request.Date;
                enrollment.State = EnrollmentState.Done;
                await enrollmentRepository.Update(enrollment);
                //await gradeRepository.Update(gradeToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The student or the subject was not found...");
            }
        }
    }
}
