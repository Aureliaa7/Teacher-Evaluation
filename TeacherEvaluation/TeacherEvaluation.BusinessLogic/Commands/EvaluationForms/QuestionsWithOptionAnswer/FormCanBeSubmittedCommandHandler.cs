using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class FormCanBeSubmittedCommandHandler : IRequestHandler<FormCanBeSubmittedCommand, bool>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<Subject> subjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Form> formRepository;
        private readonly IQuestionWithOptionAnswerRepository questionRepository;
        private readonly IAttendanceRepository attendanceRepository;

        public FormCanBeSubmittedCommandHandler(IEnrollmentRepository enrollmentRepository, IRepository<Subject> subjectRepository,
            IStudentRepository studentRepository, IRepository<ApplicationUser> userRepository, IQuestionWithOptionAnswerRepository questionRepository,
            IRepository<Form> formRepository, IAttendanceRepository attendanceRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
            this.questionRepository = questionRepository;
            this.formRepository = formRepository;
            this.attendanceRepository = attendanceRepository;
        }

        public async Task<bool> Handle(FormCanBeSubmittedCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserIdForStudent);
            if (userExists)
            {
                bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);
                bool formExists = await formRepository.Exists(x => x.Id == request.FormId);
                if (subjectExists && formExists)
                {
                    var form = await formRepository.Get(request.FormId);
                    var student = await studentRepository.GetByUserId(request.UserIdForStudent);
                    var enrollmentExists = await enrollmentRepository.Exists(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                                  x.State == request.EnrollmentState && 
                                                                                  x.TaughtSubject.Type == request.SubjectType && 
                                                                                  x.Student.Id == student.Id);

                    if (enrollmentExists)
                    {
                        var enrollment = await enrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                            request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
                        var attendances = await attendanceRepository.GetAttendancesWithRelatedEntities(enrollment.Id);
                        var questionsForForm = await questionRepository.GetQuestionsWithRelatedEntities(request.FormId);
                        var formSubmittedForEnrollment = questionsForForm.Where(x => x.Answers.Where(y => y.Enrollment.Id == enrollment.Id).Any()).Any();
                        bool minNumberOfAttendances = (attendances.Count() >= form.MinNumberOfAttendances) ? true : false;
                        if (!formSubmittedForEnrollment && minNumberOfAttendances)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
