using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models.Baithuchanh12;
[Table("Device")]
public class Device
{
    [Key]
    
    public string Device_ID { get; set; }
    public string DeviceName { get; set; }
    public string Type_ID { get; set; }
    [ForeignKey("Type_ID")]
    public DeviceType? DeviceTypes { get; set; }
    
    
    public int Remain { get; set; }
    
    public decimal Cost{ get; set; }
    public string? SupplierID { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier ?Supplier { get; set; }
    public ICollection<ReceiptDetails>? ReceiptDetails{ get; set; }
      public ICollection<IssueNoteDetails>? IssueNoteDetails{ get; set; }
}