using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Extensions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    class InsertTaughtSubjectsFromFileCommandHandler : IRequestHandler<InsertTaughtSubjectsFromFileCommand, List<string>>
    {
        private readonly IMediator mediator;

        public InsertTaughtSubjectsFromFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(InsertTaughtSubjectsFromFileCommand request, CancellationToken cancellationToken)
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
                        if (workSheet.IsExcelRowValid(i))
                        {
                            var command = new AssignSubjectCommand
                            {
                                SubjectId = new Guid(workSheet.Cells[i, 1].Value.ToString()),
                                TeacherId = new Guid(workSheet.Cells[i, 2].Value.ToString()),
                                MaxNumberOfAttendances = Convert.ToInt32(workSheet.Cells[i, 4].Value.ToString())
                            };

                            bool isValidType = Enum.TryParse(workSheet.Cells[i, 3].Value.ToString().ConvertFirstLetterToUpperCase(),
                                out TaughtSubjectType type);
                            if(isValidType)
                            {
                                command.Type = type;
                                await mediator.Send(command);
                            }
                            else
                            {
                                errors.Add($"Row {i} from {workSheet.Name} is invalid! There is no subject type named" +
                                    $"{workSheet.Cells[i, 3].Value}");
                            }
                        }
                        else
                        {
                            errors.Add($"Row {i} from {workSheet.Name} is invalid! There may be missing data.");
                        }
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
