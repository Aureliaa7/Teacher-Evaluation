using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class SaveEvaluationFormResponsesCommandHandler : AsyncRequestHandler<SaveEvaluationFormResponsesCommand>
    {
        private readonly IRepository<Form> formRepository;
        private readonly IQuestionWithOptionAnswerRepository questionRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IStudentRepository studentRepository;

        public SaveEvaluationFormResponsesCommandHandler(IRepository<Form> formRepository, IQuestionWithOptionAnswerRepository questionRepository,
            IEnrollmentRepository enrollmentRepository, IRepository<ApplicationUser> userRepository, IStudentRepository studentRepository)
        {
            this.formRepository = formRepository;
            this.questionRepository = questionRepository;
            this.enrollmentRepository = enrollmentRepository;
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
        }

        protected override async Task Handle(SaveEvaluationFormResponsesCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await formRepository.Exists(x => x.Id == request.FormId);
            bool userExists = await userRepository.Exists(x => x.Id == request.UserIdForStudent);
            if (formExists && userExists)
            {
                var student = await studentRepository.GetByUserId(request.UserIdForStudent);
                var enrollmentExists = await enrollmentRepository.Exists(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                                              x.State == request.EnrollmentState &&
                                                                              x.TaughtSubject.Type == request.SubjectType &&
                                                                              x.Student.Id == student.Id);
                if (enrollmentExists)
                {
                    var enrollment = await enrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                        request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
    
                    int contor = 0;
                    foreach(var question in request.Questions)
                    {
                        try
                        {
                            var response = new AnswerToQuestionWithOption { Answer = request.Responses[contor++], Enrollment = enrollment };
                            question.Answers.Add(response);
                            await questionRepository.Update(question);
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
