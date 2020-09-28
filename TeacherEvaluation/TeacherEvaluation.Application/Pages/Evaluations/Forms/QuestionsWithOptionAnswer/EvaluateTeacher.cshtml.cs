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
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms.QuestionsWithOptionAnswer
{
    [Authorize(Roles="Student")]
    public class EvaluateTeacherModel : PageModel
    {
        private readonly IMediator mediator;
        private Form form;

        public IEnumerable<QuestionWithOptionAnswer> Questions { get; set; }
        public List<SelectListItem> Subjects { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Answer is required")]
        [EnumDataType(typeof(OptionAnswer))]
        public List<OptionAnswer> Answers  { get; set; }

        public EvaluateTeacherModel(IMediator mediator)
        {
            this.mediator = mediator;
            Questions = new List<QuestionWithOptionAnswer>();
            try
            {
                GetEvaluationFormCommand command = new GetEvaluationFormCommand();
                form = mediator.Send(command).Result;
                GetQuestionsForFormCommand getQuestionsCommand = new GetQuestionsForFormCommand { FormId = form.Id };
                Questions = mediator.Send(getQuestionsCommand).Result;
            }
            catch (NoEvaluationFormException)
            {
                RedirectToPage("/Errors/NoEvaluationForm");
            }
        }


        public async Task<IActionResult> OnGet()
        {
            Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            GetSubjectsForEnrollmentsCommand getSubjectsCommand = new GetSubjectsForEnrollmentsCommand
            {
                UserId = userIdStudent,
                EnrollmentState = form.EnrollmentState
            };
            try { 
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

        public IActionResult OnGetReturnTeacher(string subjectId, string type)
        {
            Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid subjectIdGuid = new Guid(subjectId);
            TaughtSubjectType taughtSubjectType = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type);
            GetTeacherByCriteriaCommand command = new GetTeacherByCriteriaCommand {
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
            catch(ItemNotFoundException e)
            {
                return new JsonResult(e.Message);
            }
        }

        public IActionResult OnGetEnableOrDisableSubmitBtn(string subjectId, string type)
        {
            Guid userIdStudent = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid subjectIdGuid = new Guid(subjectId);
            TaughtSubjectType taughtSubjectType = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type);
            FormCanBeSubmittedCommand formCanBeSubmittedCommand = new FormCanBeSubmittedCommand
            {
                SubjectId = subjectIdGuid,
                UserIdForStudent = userIdStudent,
                EnrollmentState = form.EnrollmentState,
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
            catch (ItemNotFoundException e)
            {
                return new JsonResult(e.Message);
            }
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
                UserIdForStudent = userIdStudent
            };
            try
            {
                await mediator.Send(command);
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToPage("/Evaluations/Forms/QuestionsWithOptionAnswer/EvaluateTeacher");
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("/Errors/404");
            }
            return RedirectToPage("/Evaluations/Forms/QuestionsWithOptionAnswer/EvaluateTeacher");
        }
    }
}