using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    class InsertFromFileCommandHandler : IRequestHandler<InsertFromFileCommand, List<string>>
    {
        private readonly IMediator mediator;

        public InsertFromFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(InsertFromFileCommand request, CancellationToken cancellationToken)
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
                        var command = new AssignSubjectCommand
                        {
                            SubjectId = new Guid(workSheet.Cells[i, 1].Value.ToString()),
                            TeacherId = new Guid(workSheet.Cells[i, 2].Value.ToString()),
                            MaxNumberOfAttendances = Convert.ToInt32(workSheet.Cells[i, 4].Value.ToString())
                        };

                        Enum.TryParse(workSheet.Cells[i, 3].Value.ToString(), out TaughtSubjectType type);
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
