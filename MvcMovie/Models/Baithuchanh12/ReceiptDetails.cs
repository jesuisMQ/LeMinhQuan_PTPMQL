using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.Baithuchanh12;
[Table("ReceiptDetails")]
public class ReceiptDetails
{
    [Key]
    public int ID { get; set; }

    public string? Device_ID { get; set; }
    [ForeignKey("Device_ID")]
    public Device? Device { get; set; }

    public string? SupplierID { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier? Supplier { get; set; }

    public string? Type_ID { get; set; }
    [ForeignKey("DeviceType")]
    public DeviceType? DeviceTypes { get; set; }

    public string? ReceiptID { get; set; }
    [ForeignKey("ReceiptID")]
    public Receipt? Receipts { get; set; }

    public int Quantity{ get; set; }
    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal Cost { get; set; }
    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal SumAll { get; set; }

}