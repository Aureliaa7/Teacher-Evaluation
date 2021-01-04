using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private IAttendanceRepository attendanceRepository;
        private IEnrollmentRepository enrollmentRepository;
        private IFormRepository formRepository;
        private IQuestionWithOptionAnswerRepository questionWithOptionAnswerRepository;
        private ISpecializationRepository specializationRepository;
        private IStudentRepository studentRepository;
        private ITeacherRepository teacherRepository;
        private ITaughtSubjectRepository taughtSubjectRepository;
        private IRepository<ApplicationUser> userRepository;
        private IRepository<Subject> subjectRepository;
        private IRepository<StudyDomain> studyDomainRepository;
        private IRepository<Grade> gradeRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IAttendanceRepository AttendanceRepository
        {
            get
            {
                if (attendanceRepository == null)
                {
                    attendanceRepository = new AttendanceRepository(dbContext);
                }
                return attendanceRepository;
            }
        }

        public IEnrollmentRepository EnrollmentRepository
        {
            get
            {
                if (enrollmentRepository == null)
                {
                    enrollmentRepository = new EnrollmentRepository(dbContext);
                }
                return enrollmentRepository;
            }
        }

        public IFormRepository FormRepository
        {
            get
            {
                if (formRepository == null)
                {
                    formRepository = new FormRepository(dbContext);
                }
                return formRepository;
            }
        }

        public IQuestionWithOptionAnswerRepository QuestionWithOptionAnswerRepository
        {
            get
            {
                if (questionWithOptionAnswerRepository == null)
                {
                    questionWithOptionAnswerRepository = new QuestionWithOptionAnswerRepository(dbContext);
                }
                return questionWithOptionAnswerRepository;
            }
        }

        public ISpecializationRepository SpecializationRepository
        {
            get
            {
                if (specializationRepository == null)
                {
                    specializationRepository = new SpecializationRepository(dbContext);
                }
                return specializationRepository;
            }
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepository(dbContext);
                }
                return studentRepository;
            }
        }

        public ITeacherRepository TeacherRepository
        {
            get
            {
                if (teacherRepository == null)
                {
                    teacherRepository = new TeacherRepository(dbContext);
                }
                return teacherRepository;
            }
        }

        public ITaughtSubjectRepository TaughtSubjectRepository
        {
            get
            {
                if (taughtSubjectRepository == null)
                {
                    taughtSubjectRepository = new TaughtSubjectRepository(dbContext);
                }
                return taughtSubjectRepository;
            }
        }
        public IRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new Repository<ApplicationUser>(dbContext);
                }
                return userRepository;
            }
        }

        public IRepository<Subject> SubjectRepository
        {
            get
            {
                if (subjectRepository == null)
                {
                    subjectRepository = new Repository<Subject>(dbContext);
                }
                return subjectRepository;
            }
        }

        public IRepository<StudyDomain> StudyDomainRepository
        {
            get
            {
                if (studyDomainRepository == null)
                {
                    studyDomainRepository = new Repository<StudyDomain>(dbContext);
                }
                return studyDomainRepository;
            }
        }

        public IRepository<Grade> GradeRepository
        {
            get
            {
                if (gradeRepository == null)
                {
                    gradeRepository = new Repository<Grade>(dbContext);
                }
                return gradeRepository;
            }
        }
    }
}
