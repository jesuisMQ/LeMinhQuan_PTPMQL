using MvcMovie.Models.Entities;
using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models.ViewModels;
public class StudentSearchVM : IValidatableObject
{
    public string? StudentCode { get; set; }

    public string? FacultyID { get; set; }

    public StudentStatus? Status { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(StudentCode)
            && string.IsNullOrWhiteSpace(FacultyID)
            && Status == null)
        {
            yield return new ValidationResult(
                "Nhập StudentCode hoặc chọn Faculty/Status"
                );
        }
    }
}