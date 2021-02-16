using System.Threading.Tasks;
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
        IQuestionRepository QuestionRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ITaughtSubjectRepository TaughtSubjectRepository { get; }
        IRepository<ApplicationUser> UserRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IStudyDomainRepository StudyDomainRepository { get; }
        IRepository<Grade> GradeRepository { get; }
        IAnswerToQuestionWithOptionRepository AnswerToQuestionWithOptionRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
