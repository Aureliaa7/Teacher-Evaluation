using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class SaveEvaluationFormResponsesCommandHandler : AsyncRequestHandler<SaveEvaluationFormResponsesCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public SaveEvaluationFormResponsesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(SaveEvaluationFormResponsesCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(x => x.Id == request.FormId);
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserIdForStudent);
            if (formExists && userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserIdForStudent);
                var enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                              x.Semester == request.Semester &&
                                                                              x.TaughtSubject.Type == request.SubjectType &&
                                                                              x.Student.Id == student.Id);
                if (enrollmentExists)
                {
                    var enrollment = await unitOfWork.EnrollmentRepository.GetEnrollmentBySubjectSemesterTypeAndStudentAsync(
                        request.SubjectId, request.Semester, request.SubjectType, student.Id);

                    int contor = 0;
                    foreach (var question in request.Questions.LikertQuestions)
                    {
                        var response = new AnswerToQuestionWithOption
                        {
                            Score = request.Scores[contor++],
                            Enrollment = enrollment,
                            Question = question
                        };
                        await unitOfWork.AnswerToQuestionWithOptionRepository.AddAsync(response);
                    }
 
                    contor = 0;
                    foreach (var question in request.Questions.FreeFormQuestions)
                    {
                        var response = new AnswerToQuestionWithText
                        {
                            FreeFormAnswer = request.FreeFormAnswers[contor++],
                            Enrollment = enrollment,
                            Question = question,
                            IsFreeForm = true
                        };
                        await unitOfWork.AnswerToQuestionWithTextRepository.AddAsync(response);
                    }
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new ItemNotFoundException("The enrollment was not found...");
                }
            }
            else
            {
                throw new ItemNotFoundException("The evaluation form was not found...");
            }
        }
    }
}
