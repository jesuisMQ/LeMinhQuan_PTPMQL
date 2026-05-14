using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.Baithuchanh12;
[Table("Receipts")]
public class Receipt
{
    [Key]
    public string ReceiptID { get; set; }
    public DateTime TimeStamp { get; set; }
    public ICollection<ReceiptDetails>? ReceiptDetails { get; set; }
    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public string SupplierID { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier Supplier { get; set; }
    public int Total { get; set; }
}