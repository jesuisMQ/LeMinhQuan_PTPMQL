using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.Text;
namespace MvcMovie.Models.Baithuchanh9;

public class SanPham
{
    [Key]
    [Required(ErrorMessage = "Mã sản phẩm k dc để trống")]
    public string ProductID { get; set; }
    public string ProductName { get; set; }

    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal Price { get; set; }
    public int Remain { get; set; }
    public ICollection<ChitietDH>? Details { get; set; }
}