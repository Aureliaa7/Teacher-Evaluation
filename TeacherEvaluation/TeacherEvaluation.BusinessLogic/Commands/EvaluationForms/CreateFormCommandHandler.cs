using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateFormCommandHandler : AsyncRequestHandler<CreateFormCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly INotificationService notificationService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateFormCommandHandler(
            IUnitOfWork unitOfWork, 
            INotificationService notificationService,
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.notificationService = notificationService;
            this.userManager = userManager;
        }

        protected override async Task Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            Form form = new Form
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EnrollmentState = EnrollmentState.InProgress,
                MinNumberOfAttendances = request.MinNumberOfAttendances
            };
            await unitOfWork.FormRepository.AddAsync(form);

            var freeFormQuestions = request.Questions.TakeLast(Constants.NumberOfFreeFormQuestions);
            await SaveQuestions(freeFormQuestions, true, form);
            var questionsWithAnswerOption = request.Questions.Take(Constants.NumberOfLikertQuestions);
            await SaveQuestions(questionsWithAnswerOption, false, form);
            await unitOfWork.SaveChangesAsync();

            // notify students
            string startDate = request.StartDate.ToString("yyyy-MM-dd hh:mm:ss");
            string endDate = request.EndDate.ToString("yyyy-MM-dd hh:mm:ss");
            string interval = string.Join(" - ", startDate, endDate);
            Notification notification = ConfigureNotificationMessage(interval);
            notificationService.Send(notification);
        }

        private async Task SaveQuestions(IEnumerable<string> questions, bool haveTextAnswer, Form form)
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

        private Notification ConfigureNotificationMessage(string interval)
        {
            Notification notification = CreateEvaluateTeacherNotification(interval);

            IList<NotificationAddress> recipients = new List<NotificationAddress>();
            GetStudentsAddresses(recipients);
            AddMessageRecipients(recipients, notification);
            AddMessageSender(notification);
            return notification;
        }

        private Notification CreateEvaluateTeacherNotification(string interval)
        {
            string bodyResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);

            return new Notification
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, interval)
            };
        }

        private void GetStudentsAddresses(IList<NotificationAddress> recipients)
        {
            IList<ApplicationUser> users = userManager.GetUsersInRoleAsync("Student").Result.ToList();

            foreach (ApplicationUser user in users)
            {
                string name = string.Join(" ", user.LastName, user.FathersInitial, user.FirstName);
                NotificationAddress recipient = new NotificationAddress(name, user.Email);
                recipients.Add(recipient);
            }
        }

        private void AddMessageRecipients(IList<NotificationAddress> recipients, Notification notification)
        {
            foreach (NotificationAddress recipient in recipients)
            {
                notification.ToAddresses.Add(recipient);
            }
        }

        private void AddMessageSender(Notification notification)
        {
            notification.FromAddress = new NotificationAddress("noreply-teacher.evaluation", "teacher.evaluation.project2021@gmail.com");
        }
    }
}