using OfficeOpenXml;

namespace TeacherEvaluation.BusinessLogic.Extensions
{
    static class ExcelWorksheetExtension
    {
        public static bool IsExcelRowValid(this ExcelWorksheet worksheet, int row)
        {
            for (int i = 1; i <= worksheet.Dimension.Columns; i++)
            {
                if (worksheet.Cells[row, i].Value == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
