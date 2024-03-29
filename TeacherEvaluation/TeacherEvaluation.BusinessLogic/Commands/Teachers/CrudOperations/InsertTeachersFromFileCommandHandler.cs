﻿using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Extensions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    class InsertTeachersFromFileCommandHandler : IRequestHandler<InsertTeachersFromFileCommand, List<string>>
    {
        private readonly IMediator mediator;

        public InsertTeachersFromFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(InsertTeachersFromFileCommand request, CancellationToken cancellationToken)
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
                    if (workSheet.IsExcelRowValid(i))
                    {
                        var command = new TeacherRegistrationCommand
                        {
                            FirstName = workSheet.Cells[i, 1].Value.ToString(),
                            LastName = workSheet.Cells[i, 2].Value.ToString(),
                            FathersInitial = workSheet.Cells[i, 3].Value.ToString(),
                            Email = workSheet.Cells[i, 4].Value.ToString(),
                            PIN = workSheet.Cells[i, 5].Value.ToString(),
                            Password = workSheet.Cells[i, 6].Value.ToString(),
                            ConfirmationUrlTemplate = request.ConfirmationUrlTemplate,
                            Degree = workSheet.Cells[i, 7].Value.ToString()
                        };
                        bool isValidDepartment = Enum.TryParse(workSheet.Cells[i, 8].Value.ToString().ToUpper(), out Department department);
                        if(isValidDepartment)
                        {
                            command.Department = department;
                            var _errors = await mediator.Send(command);
                            if (_errors != null)
                            {
                                errors.AddRange(_errors);
                            }
                        }
                        else
                        {
                            errors.Add($"Row {i} from {workSheet.Name} is invalid! There is no department named" +
                                $"{workSheet.Cells[i, 8].Value}");
                        }
                    }
                    else
                    {
                        errors.Add($"Row {i} from {workSheet.Name} is invalid! There may be missing data.");
                    }
                }
            }
            return errors;
        }
    }
}
