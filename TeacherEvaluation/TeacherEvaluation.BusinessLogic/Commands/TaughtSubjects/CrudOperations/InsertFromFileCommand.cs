using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class InsertFromFileCommand : IRequest<List<string>>
    {
        public IFormFile ExcelFile { get; set; }
    }
}
