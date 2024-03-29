﻿using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Extensions;
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
                        if (workSheet.IsExcelRowValid(i))
                        {
                            var command = new UpdateGradeCommand
                            {
                                StudentId = new Guid(workSheet.Cells[i, 1].Value.ToString()),
                                SubjectId = new Guid(workSheet.Cells[i, 2].Value.ToString()),
                                Value = Convert.ToInt32(workSheet.Cells[i, 3].Value.ToString()),
                                Date = DateTime.Parse(workSheet.Cells[i, 4].Value.ToString()),
                            };
                            bool isValidSubjectType = Enum.TryParse(workSheet.Cells[i, 5].Value.ToString().ConvertFirstLetterToUpperCase(),
                                out TaughtSubjectType type);
                            if (isValidSubjectType)
                            {
                                command.Type = type;
                                await mediator.Send(command);
                            }
                            else
                            {
                                errors.Add($"Row {i} from {workSheet.Name} is invalid! There is no subject type named" +
                                   $"{workSheet.Cells[i, 5].Value}");
                            }
                        }
                        else
                        {
                            errors.Add($"Row {i} from {workSheet.Name} is invalid! There may be missing data.");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {i} from {workSheet.Name}: {ex.Message}");
                    }
                }
            }
            return errors;
        }
    }
}
