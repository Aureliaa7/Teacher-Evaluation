using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    class InsertStudentsFromFileCommandHandler : IRequestHandler<InsertStudentsFromFileCommand, List<string>>
    {
        private readonly IMediator mediator;

        public InsertStudentsFromFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(InsertStudentsFromFileCommand request, CancellationToken cancellationToken)
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

                // i starts from 2 because the first row contains the column names
                for (int i = 2; i <= totalRows; i++)
                {
                    try
                    {
                        var command = new StudentRegistrationCommand
                        {
                            FirstName = workSheet.Cells[i, 1].Value.ToString(),
                            LastName = workSheet.Cells[i, 2].Value.ToString(),
                            FathersInitial = workSheet.Cells[i, 3].Value.ToString(),
                            Email = workSheet.Cells[i, 4].Value.ToString(),
                            PIN = workSheet.Cells[i, 5].Value.ToString(),
                            Group = workSheet.Cells[i, 6].Value.ToString(),
                            Password = workSheet.Cells[i, 7].Value.ToString(),
                            SpecializationId = new Guid(workSheet.Cells[i, 8].Value.ToString()),
                            StudyYear = Convert.ToInt16(workSheet.Cells[i, 9].Value),
                            ConfirmationUrlTemplate = request.ConfirmationUrlTemplate
                        };

                        var _errors = await mediator.Send(command);
                        if (_errors != null)
                        {
                            errors.AddRange(_errors);
                        }
                    }
                    catch(Exception ex)
                    {
                        errors.Add(ex.Message);
                    }
                }
            }
            return errors;
        }
    }
}
