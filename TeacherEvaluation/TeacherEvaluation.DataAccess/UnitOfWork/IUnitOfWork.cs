using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAttendanceRepository AttendanceRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IFormRepository FormRepository { get; }
        IQuestionWithOptionAnswerRepository QuestionWithOptionAnswerRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ITaughtSubjectRepository TaughtSubjectRepository { get; }
        IRepository<ApplicationUser> UserRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IRepository<StudyDomain> StudyDomainRepository { get; }
        IRepository<Grade> GradeRepository { get; }
    }
}
