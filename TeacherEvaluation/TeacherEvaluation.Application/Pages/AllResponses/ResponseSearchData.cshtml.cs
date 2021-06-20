using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.AllResponses
{
    public class ResponseSearchDataModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public Guid FormId { get; set; }

        [BindProperty]
        public string SelectedSubjectId { get; set; }

        public ResponseSearchDataModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<SelectListItem>> GetAllTeachersAsync()
        {
            var teachers = new List<SelectListItem>();
            try
            {
                GetAllTeachersCommand getTeachersCommand = new GetAllTeachersCommand();
                var _teachers = await mediator.Send(getTeachersCommand);
                var orderedTeachers = _teachers.OrderBy(t => t.User.FirstName).ThenBy(t => t.User.LastName);

                teachers = orderedTeachers.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.User.FirstName + " " + x.User.LastName
                }).ToList();
            }
            catch (ItemNotFoundException) { }
            return teachers;
        }
    }
}
