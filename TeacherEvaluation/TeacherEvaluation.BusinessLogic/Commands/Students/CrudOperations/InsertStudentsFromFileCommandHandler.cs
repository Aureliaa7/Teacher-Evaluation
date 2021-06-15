using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Extensions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    class InsertStudentsFromFileCommandHandler : IRequestHandler<InsertStudentsFromFileCommand, List<string>>
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;

        public InsertStudentsFromFileCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
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
                        if (workSheet.IsExcelRowValid(i))
                        {
                            string specializationName = workSheet.Cells[i, 8].Value.ToString();
                            var specialization = await unitOfWork.SpecializationRepository.GetByNameAsync(specializationName);
                            if (specialization != null)
                            {
                                var a = workSheet.Cells[i, 1]?.Value.ToString();
                                var command = new StudentRegistrationCommand
                                {
                                    FirstName = workSheet.Cells[i, 1].Value.ToString(),
                                    LastName = workSheet.Cells[i, 2].Value.ToString(),
                                    FathersInitial = workSheet.Cells[i, 3].Value.ToString(),
                                    Email = workSheet.Cells[i, 4].Value.ToString(),
                                    PIN = workSheet.Cells[i, 5].Value.ToString(),
                                    Group = workSheet.Cells[i, 6].Value.ToString(),
                                    Password = workSheet.Cells[i, 7].Value.ToString(),
                                    SpecializationId = specialization.Id,
                                    StudyYear = Convert.ToInt16(workSheet.Cells[i, 9].Value),
                                    ConfirmationUrlTemplate = request.ConfirmationUrlTemplate
                                };

                                var _errors = await mediator.Send(command);
                                if (_errors != null)
                                {
                                    errors.AddRange(_errors);
                                }
                            }
                            else
                            {
                                errors.Add($"Specialization {specializationName} was not found!");
                            }
                        }
                        else 
                        {
                            errors.Add($"Row {i} from {workSheet.Name} is invalid! There may be missing data.");
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
