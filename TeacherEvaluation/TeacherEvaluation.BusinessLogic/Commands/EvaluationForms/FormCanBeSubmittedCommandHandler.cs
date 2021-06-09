using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
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
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserIdForStudent);
            bool formCanBeSubmitted = false;

            if (userExists)
            {
                bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.SubjectId);
                bool formExists = await unitOfWork.FormRepository.ExistsAsync(x => x.Id == request.FormId);
                if (subjectExists && formExists)
                {
                    var form = await unitOfWork.FormRepository.GetAsync(request.FormId);
                    var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserIdForStudent);
                    var enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                                  x.State == EnrollmentState.InProgress && 
                                                                                  x.TaughtSubject.Type == request.SubjectType && 
                                                                                  x.Student.Id == student.Id);

                    if (enrollmentExists)
                    {
                        var enrollment = await unitOfWork.EnrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudentAsync(
                            request.SubjectId, EnrollmentState.InProgress, request.SubjectType, student.Id);

                        var answersForForm = await unitOfWork.AnswerToQuestionWithOptionRepository.GetByEnrollmentAndFormIdAsync(enrollment.Id, request.FormId);
                        var formSubmittedForEnrollment = answersForForm.Any();
                        bool minNumberOfAttendances = (enrollment.NumberOfAttendances >= form.MinNumberOfAttendances) ? true : false;
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
