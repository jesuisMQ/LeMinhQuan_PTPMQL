using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models.Baithuchanh12;
[Table("Suppliers")]
public class Supplier
{
    
    [Key]
    [Required(ErrorMessage = "Mã nhà cung cấp k đc để trống")]
    [RegularExpression(@"^[0-9a-zA-Z\s]+$", ErrorMessage = "Không được nhập kí tự đặc biệt")]
    [StringLength(10, ErrorMessage = "Mã NCC tối đa 50 ký tự")]
    public string SupplierID { get; set; }
    [Required (ErrorMessage ="Tên NCC không được để trống")]
    [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Chỉ được nhập chữ")]
    [StringLength(50, ErrorMessage = "Tên NCC tối đa 50 ký tự")]
    public string SupplierName { get; set; }
    public ICollection<Receipt>? Receipts { get; set; }
     public ICollection<IssueNote>? IssuNotes { get; set; }
     public ICollection<IssueNoteDetails>? IssuNoteDetails { get; set; }
     public ICollection<ReceiptDetails>? ReceiptDetails { get; set; }
}
