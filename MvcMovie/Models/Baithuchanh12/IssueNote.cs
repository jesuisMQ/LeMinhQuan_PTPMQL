
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models.Baithuchanh12;
[Table("IssueNotes")]
public class IssueNote
{
    [Key]
    public string IN_ID { get; set; }
    public DateTime TimeStamp { get; set; }
    public ICollection<ReceiptDetails>? ReceiptDetails { get; set; }
    public string SupplierID { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier? Supplier { get; set; }
    public int Total { get; set; }
}