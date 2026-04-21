using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.Text;
using MvcMovie.Models.ViewModels;
namespace MvcMovie.Models.Baithuchanh9;

public class DonHang
{
    [Key]
    public string OrderID { get; set; }
    public string CustomerID { get; set; } = default!;
    [ForeignKey("CustomerID")]
    public KhachHang? Customer { get; set; }
    public ICollection<ChitietDH> Details { get; set; }
    public DateTime Timestamp { get; set; }

    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal Total { get; set; }

}