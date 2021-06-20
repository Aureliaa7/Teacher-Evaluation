﻿using MediatR;
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
        private readonly INotificationService notificationService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateEvaluationFormCommandHandler(
            IUnitOfWork unitOfWork, 
            INotificationService notificationService,
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

            IList<NotificationAddress> recipients = GetStudentsAddresses();
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

        private IList<NotificationAddress> GetStudentsAddresses()
        {
            IList<ApplicationUser> users = userManager.GetUsersInRoleAsync("Student").Result.ToList();
            IList<NotificationAddress> notificationAddresses = new List<NotificationAddress>();
            foreach (ApplicationUser user in users)
            {
                string name = string.Join(" ", user.LastName, user.FathersInitial, user.FirstName);
                NotificationAddress notificationAddress = new NotificationAddress(name, user.Email);
                notificationAddresses.Add(notificationAddress);
            }
            return notificationAddresses;
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