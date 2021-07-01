using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.EmailSender.NotificationService.Interfaces;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateEvaluationFormCommandHandler : AsyncRequestHandler<CreateEvaluationFormCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailService notificationService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateEvaluationFormCommandHandler(
            IUnitOfWork unitOfWork, 
            IEmailService notificationService,
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.notificationService = notificationService;
            this.userManager = userManager;
        }

        protected override async Task Handle(CreateEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            Form form = new Form
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Semester = request.Semester,
                MinNumberOfAttendances = request.MinNumberOfAttendances
            };
            await unitOfWork.FormRepository.AddAsync(form);

            var freeFormQuestions = request.Questions.TakeLast(Constants.NumberOfFreeFormQuestions);
            await AddQuestions(freeFormQuestions, true, form);
            var questionsWithAnswerOption = request.Questions.Take(Constants.NumberOfLikertQuestions);
            await AddQuestions(questionsWithAnswerOption, false, form);
            await unitOfWork.SaveChangesAsync();

            // notify students
            string startDate = request.StartDate.ToString("yyyy-MM-dd hh:mm:ss");
            string endDate = request.EndDate.ToString("yyyy-MM-dd hh:mm:ss");
            string interval = string.Join(" - ", startDate, endDate);
            EmailMessage notification = ConfigureNotificationMessage(interval);
            await notificationService .SendAsync(notification);
        }

        private async Task AddQuestions(IEnumerable<string> questions, bool haveTextAnswer, Form form)
        {
            foreach (var question in questions)
            {
                Question newQuestion = new Question
                {
                    Text = question,
                    Form = form,
                    HasFreeFormAnswer = haveTextAnswer
                };
                await unitOfWork.QuestionRepository.AddAsync(newQuestion);
            }
        }

        private EmailMessage ConfigureNotificationMessage(string interval)
        {
            EmailMessage notification = CreateEvaluateTeacherNotification(interval);

            IList<EmailAddress> toAddresses = GetStudentsAddresses();

            ((List<EmailAddress>)notification.ToAddresses).AddRange(toAddresses);
            notification.FromAddress = new EmailAddress { Name = "noreply-teacher.evaluation", Address = "teacher.evaluation.project2021@gmail.com" };
            return notification;
        }

        private EmailMessage CreateEvaluateTeacherNotification(string interval)
        {
            string bodyResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);

            return new EmailMessage
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, interval)
            };
        }

        private IList<EmailAddress> GetStudentsAddresses()
        {
            IList<ApplicationUser> users = userManager.GetUsersInRoleAsync("Student").Result.ToList();
            IList<EmailAddress> notificationAddresses = new List<EmailAddress>();
            foreach (ApplicationUser user in users)
            {
                string name = string.Join(" ", user.LastName, user.FathersInitial, user.FirstName);
                EmailAddress notificationAddress = new EmailAddress
                {
                    Name = name,
                    Address = user.Email
                };

                notificationAddresses.Add(notificationAddress);
            }
            return notificationAddresses;
        }
    }
}