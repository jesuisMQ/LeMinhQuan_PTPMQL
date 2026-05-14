using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.Baithuchanh12;
[Table("IssueNoteDetails")]
public class IssueNoteDetails
{
    [Key]
    public int ID { get; set; }
    public string Device_ID { get; set; }
    [ForeignKey("Device_ID")]
    public Device? Device { get; set; }
    public string SupplierID { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier? Supplier { get; set; }
    public decimal Cost{ get; set; }
    public int Quantity{ get; set; }
    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal SumAll { get; set; }

}