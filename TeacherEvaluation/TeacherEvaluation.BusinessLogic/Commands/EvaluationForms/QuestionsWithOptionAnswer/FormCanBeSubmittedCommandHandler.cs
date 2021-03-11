using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class FormCanBeSubmittedCommandHandler : IRequestHandler<FormCanBeSubmittedCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public FormCanBeSubmittedCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(FormCanBeSubmittedCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserIdForStudent);
            bool formCanBeSubmitted = false;

            if (userExists)
            {
                bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.SubjectId);
                bool formExists = await unitOfWork.FormRepository.Exists(x => x.Id == request.FormId);
                if (subjectExists && formExists)
                {
                    var form = await unitOfWork.FormRepository.Get(request.FormId);
                    var student = await unitOfWork.StudentRepository.GetByUserId(request.UserIdForStudent);
                    var enrollmentExists = await unitOfWork.EnrollmentRepository.Exists(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                                  x.State == request.EnrollmentState && 
                                                                                  x.TaughtSubject.Type == request.SubjectType && 
                                                                                  x.Student.Id == student.Id);

                    if (enrollmentExists)
                    {
                        var enrollment = await unitOfWork.EnrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                            request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
                        var attendances = await unitOfWork.AttendanceRepository.GetAttendancesWithRelatedEntities(enrollment.Id);
                        var answersForForm = await unitOfWork.AnswerToQuestionWithOptionRepository.GetByEnrollmentAndFormId(enrollment.Id, request.FormId);
                        var formSubmittedForEnrollment = answersForForm.Any();
                        bool minNumberOfAttendances = (attendances.Count() >= form.MinNumberOfAttendances) ? true : false;
                        if (!formSubmittedForEnrollment && minNumberOfAttendances)
                        {
                            formCanBeSubmitted = true;
                        }
                    }
                    return formCanBeSubmitted;
                }
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
