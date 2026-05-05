using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;
using MvcMovie.Models.Entities;
using System.Linq;
namespace MvcMovie.Models.Process;
public class ExcelProcess
{
    public static List<(Student student, List<string> errors)> ImportFromExcel(string filePath)
{
    ExcelPackage.License.SetNonCommercialPersonal("StudentProject");

    var result = new List<(Student student, List<string> errors)>();

    using (var package = new ExcelPackage(new FileInfo(filePath)))
    {
        var ws = package.Workbook.Worksheets[0];
        int rowCount = ws.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            var std = new Student
            {
                StudentCode = ws.Cells[row, 1].Text,
                FullName = ws.Cells[row, 2].Text,
                Email = ws.Cells[row, 3].Text
            };

            if (string.IsNullOrWhiteSpace(std.StudentCode))
                continue;

            var context = new ValidationContext(std);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(std, context, validationResults, true);

            var errors = validationResults
                .Select(e => $"Row {row}: {e.ErrorMessage}")
                .ToList();

            result.Add((std, errors));
        }
    }

    return result;
}
}