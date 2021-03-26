using MediatR;
using System;
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
            bool formExists = await unitOfWork.FormRepository.Exists(x => x.Id == request.FormId);
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserIdForStudent);
            if (formExists && userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserId(request.UserIdForStudent);
                var enrollmentExists = await unitOfWork.EnrollmentRepository.Exists(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                              x.State == request.EnrollmentState &&
                                                                              x.TaughtSubject.Type == request.SubjectType &&
                                                                              x.Student.Id == student.Id);
                if (enrollmentExists)
                {
                    var enrollment = await unitOfWork.EnrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                        request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
    
                    int contor = 0;
                    foreach(var question in request.Questions)
                    {
                        try
                        {
                            var response = new AnswerToQuestionWithOption { Answer = request.Responses[contor++], Enrollment = enrollment, Question = question };
                            await unitOfWork.AnswerToQuestionWithOptionRepository.Add(response);
                            unitOfWork.QuestionRepository.Update(question);
                            await unitOfWork.SaveChangesAsync();
                        }
                        catch (ArgumentOutOfRangeException) { throw; }
                    }
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
