using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
namespace MvcMovie.Models.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Mã sinh viên từ 3 đến 10 ký tự")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Chỉ được nhập số")]
        public string StudentCode { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Chỉ được nhập chữ")]
        [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        public string? FacultyID { get; set; } = default!;
        [ForeignKey("FacultyID")]
        public virtual Faculty? Faculty { get; set; } = default!;       
    }
}
