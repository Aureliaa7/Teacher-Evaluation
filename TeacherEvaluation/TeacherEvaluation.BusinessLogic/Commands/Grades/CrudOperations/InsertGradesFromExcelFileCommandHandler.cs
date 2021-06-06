using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    class InsertGradesFromExcelFileCommandHandler : IRequestHandler<InsertGradesFromExcelFileCommand, List<string>>
    {
        private readonly IMediator mediator;

        public InsertGradesFromExcelFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(InsertGradesFromExcelFileCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var newfile = new FileInfo(request.ExcelFile.FileName);
            var fileExtension = newfile.Extension;

            if (!Constants.ExcelExtensions.Contains(fileExtension))
            {
                throw new Exception($"{newfile} is not an excel file!");
            }

            using MemoryStream stream = new MemoryStream();
            await request.ExcelFile.CopyToAsync(stream);

            using ExcelPackage package = new ExcelPackage(stream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var workSheets = package.Workbook.Worksheets;
            foreach (var workSheet in workSheets)
            {
                int totalRows = workSheet.Dimension.Rows;

                for (int i = 2; i <= totalRows; i++)
                {
                    try
                    {
                        var command = new UpdateGradeCommand
                        {
                           StudentId = new Guid(workSheet.Cells[i, 1].Value.ToString()),
                           SubjectId = new Guid(workSheet.Cells[i, 2].Value.ToString()),
                           Value = Convert.ToInt32(workSheet.Cells[i, 3].Value.ToString()), 
                           Date = DateTime.Parse(workSheet.Cells[i, 4].Value.ToString()),
                        };
                        Enum.TryParse(workSheet.Cells[i, 5].Value.ToString(), out TaughtSubjectType type);
                        command.Type = type;

                        await mediator.Send(command);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                    }
                }
            }
            return errors;
        }
    }
}
