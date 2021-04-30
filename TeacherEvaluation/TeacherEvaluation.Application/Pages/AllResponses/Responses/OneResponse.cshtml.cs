using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Responses;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
 /*   [Authorize(Roles = "Dean")]
    [Authorize(Roles = "Teacher")]*/
    public class OneResponseModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public IDictionary<string, string> QuestionsAndResponses { get; set; }
        [BindProperty]
        public Guid FormId { get; set; }

        public OneResponseModel(IMediator mediator)
        {
            this.mediator = mediator;
            QuestionsAndResponses = new Dictionary<string, string>();
        }

        public async Task OnGet(Guid enrollmentId, Guid formId)
        {
            FormId = formId;
            try
            {
                ResponsesByEnrollmentAndFormCommand command = new ResponsesByEnrollmentAndFormCommand
                {
                    EnrollmentId = enrollmentId,
                    FormId = formId
                };
                QuestionsAndResponses = await mediator.Send(command);
            }
            catch(ItemNotFoundException)
            {
                throw;
            }
        }
    }
}
