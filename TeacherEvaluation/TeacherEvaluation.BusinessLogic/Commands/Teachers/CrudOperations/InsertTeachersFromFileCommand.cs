using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class InsertTeachersFromFileCommand : IRequest<List<string>>
    {
        public IFormFile ExcelFile { get; set; }
        public string ConfirmationUrlTemplate { get; set; }
    }
}
