using System.Threading.Tasks;
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
        private IEnrollmentRepository enrollmentRepository;
        private IFormRepository formRepository;
        private IQuestionRepository questionRepository;
        private ISpecializationRepository specializationRepository;
        private IStudentRepository studentRepository;
        private ITeacherRepository teacherRepository;
        private ITaughtSubjectRepository taughtSubjectRepository;
        private IRepository<ApplicationUser> userRepository;
        private ISubjectRepository subjectRepository;
        private IStudyDomainRepository studyDomainRepository;
        private IRepository<Grade> gradeRepository;
        private IAnswerToQuestionWithOptionRepository answerToQuestionWithOptionRepository;
        private IAnswerToQuestionWithTextRepository answerToQuestionWithTextRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public IQuestionRepository QuestionRepository
        {
            get
            {
                if (questionRepository == null)
                {
                    questionRepository = new QuestionRepository(dbContext);
                }
                return questionRepository;
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

        public ISubjectRepository SubjectRepository
        {
            get
            {
                if (subjectRepository == null)
                {
                    subjectRepository = new SubjectRepository(dbContext);
                }
                return subjectRepository;
            }
        }

        public IStudyDomainRepository StudyDomainRepository
        {
            get
            {
                if (studyDomainRepository == null)
                {
                    studyDomainRepository = new StudyDomainRepository(dbContext);
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

        public IAnswerToQuestionWithOptionRepository AnswerToQuestionWithOptionRepository
        {
            get
            {
                if(answerToQuestionWithOptionRepository == null)
                {
                    answerToQuestionWithOptionRepository = new AnswerToQuestionWithOptionRepository(dbContext);
                }
                return answerToQuestionWithOptionRepository;
            }
        }

        public IAnswerToQuestionWithTextRepository AnswerToQuestionWithTextRepository
        {
            get
            {
                if (answerToQuestionWithTextRepository == null)
                {
                    answerToQuestionWithTextRepository = new AnswerToQuestionWithTextRepository(dbContext);
                }
                return answerToQuestionWithTextRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
