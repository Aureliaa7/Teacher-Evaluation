using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using TeacherEvaluation.Application.Pages.Evaluations.Forms;
using TeacherEvaluation.BusinessLogic;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    [Authorize(Roles = "Student")]
    public class EvaluateTeacherModel : PageModel
    {
        private readonly IMediator mediator;
        private Form form;
        private bool formIsAvailable;

        public QuestionsVm Questions { get; set; }
        public List<SelectListItem> Subjects { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        public TaughtSubjectType Type { get; set; }

        public List<SelectListItem> AnswerOptions = new List<SelectListItem>();

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Answer is required")]
        [EnumDataType(typeof(AnswerOption))]
        public List<AnswerOption> Answers { get; set; }

        [BindProperty]
        [Required]
        public List<string> TextAnswers { get; set; }

        [BindProperty]
        public int NumberOfLikertQuestions { get; set; } = Constants.NumberOfLikertQuestions;

        [BindProperty]
        public int NumberOfFreeFormQuestions { get; set; } = Constants.NumberOfFreeFormQuestions;

        public EvaluateTeacherModel(IMediator mediator)
        {
            this.mediator = mediator;
            formIsAvailable = true;
            AnswerOptions = GetAnswerOptions();

            try
            {
                GetEvaluationFormCommand command = new GetEvaluationFormCommand();
                form = mediator.Send(command).Result;
                GetQuestionsForFormCommand getQuestionsCommand = new GetQuestionsForFormCommand { FormId = form.Id };
                Questions = mediator.Send(getQuestionsCommand).Result;
            }
            catch (AggregateException)
            {
                formIsAvailable = false;
            }
        }

        private List<SelectListItem> GetAnswerOptions()
        {
            return Enum.GetValues(typeof(AnswerOption))
               .Cast<AnswerOption>()
               .Select(x =>
               {
                   string displayText = AnswerOptionConvertor.ToDisplayString(x);
                   return new SelectListItem(displayText, x.ToString());
               })
               .ToList();
        }

        public async Task<IActionResult> OnGet()
        {
            Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (formIsAvailable)
            {
                GetSubjectsForEnrollmentsCommand getSubjectsCommand = new GetSubjectsForEnrollmentsCommand
                {
                    UserId = userIdStudent,
                    EnrollmentState = form.EnrollmentState
                };

                try
                {
                    var subjects = await mediator.Send(getSubjectsCommand);
                    Subjects = subjects.Select(x =>
                                                    new SelectListItem
                                                    {
                                                        Value = x.Id.ToString(),
                                                        Text = x.Name
                                                    }).ToList();
                    return Page();
                }

                catch (ItemNotFoundException)
                {
                    return RedirectToPage("/Errors/404");
                }
            }
            else
            {
                return RedirectToPage("/Errors/NoEvaluationForm");
            }
        }

        public IActionResult OnGetReturnTeacher(string subjectId, string type)
        {
            if (!string.IsNullOrEmpty(subjectId) && !string.IsNullOrEmpty(type))
            {
                Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Guid subjectIdGuid = new Guid(subjectId);
                TaughtSubjectType taughtSubjectType = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type);
                GetTeacherByCriteriaCommand command = new GetTeacherByCriteriaCommand
                {
                    SubjectId = subjectIdGuid,
                    UserIdForStudent = userIdStudent,
                    EnrollmentState = form.EnrollmentState,
                    SubjectType = taughtSubjectType
                };
                try
                {
                    var teacher = mediator.Send(command).Result;
                    return new JsonResult(teacher);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        public IActionResult OnGetEnableOrDisableSubmitBtn(string subjectId, string type)
        {
            if (!string.IsNullOrEmpty(subjectId) && !string.IsNullOrEmpty(type))
            {
                Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Guid subjectIdGuid = new Guid(subjectId);
                TaughtSubjectType taughtSubjectType = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type);
                FormCanBeSubmittedCommand formCanBeSubmittedCommand = new FormCanBeSubmittedCommand
                {
                    SubjectId = subjectIdGuid,
                    UserIdForStudent = userIdStudent,
                    SubjectType = taughtSubjectType,
                    FormId = form.Id
                };

                try
                {
                    var btnEnabled = mediator.Send(formCanBeSubmittedCommand).Result;

                    if (btnEnabled)
                    {
                        return new JsonResult("enable");
                    }
                    return new JsonResult("disable");
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            SaveEvaluationFormResponsesCommand command = new SaveEvaluationFormResponsesCommand
            {
                EnrollmentState = form.EnrollmentState,
                FormId = form.Id,
                Questions = Questions,
                Responses = Answers,
                SubjectId = SubjectId,
                SubjectType = Type,
                UserIdForStudent = userIdStudent,
                FreeFormAnswers = TextAnswers
            };
            try
            {
                await mediator.Send(command);
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToPage("/EvaluationForms/EvaluateTeacher");
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("/Errors/404");
            }
            return RedirectToPage("/EvaluationForms/EvaluateTeacher");
        }
    }
}