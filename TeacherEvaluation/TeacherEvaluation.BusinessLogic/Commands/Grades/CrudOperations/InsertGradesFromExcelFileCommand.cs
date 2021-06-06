using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class InsertGradesFromExcelFileCommand : IRequest<List<string>>
    {
        public IFormFile ExcelFile { get; set; }
    }
}
