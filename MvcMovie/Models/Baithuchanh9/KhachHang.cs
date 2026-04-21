using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.Text;
namespace MvcMovie.Models.Baithuchanh9;
public class KhachHang
{
    [Key]
    [Required(ErrorMessage ="Mã khách hàng k đc để trống")]
    [RegularExpression(@"^[0-9a-zA-Z\s]+$", ErrorMessage = "Không được nhập kí tự đặc biệt")]
    [StringLength(10, ErrorMessage = "Họ tên tối đa 50 ký tự")]
    public string CustomerID { get; set; }
    [Required (ErrorMessage ="Tên khách hàng không được để trống")]
    [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Chỉ được nhập chữ")]
    [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự")]
    public string CustomerName { get; set; }
    public ICollection<DonHang>? Orders { get; set; }
    
    
    
    
    
    
}