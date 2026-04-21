using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.Text;
namespace MvcMovie.Models.Baithuchanh9;

public class ChitietDH
{
    [Key]
    public int ID { get; set; }

    public string? OrderID { get; set; }
    [ForeignKey("OrderID")]
    public DonHang? Orders { get; set; }
    public string ProductID { get; set; }
    [ForeignKey("ProductID")]
    public SanPham? Products { get; set; }

    public int Quantity { get; set; }

    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal Price { get; set; }

    // tổng của dòng

    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal SumAll { get; set; }
}