
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models.Baithuchanh12;
[Table("DeviceType")]
public class DeviceType
{
    [Key]
    public string Type_ID { get; set; }
    public string TypeName { get; set; }
    public ICollection<Device>? Devices { get; set; }
     public ICollection<DeviceType>? DeviceTypes { get; set; }

}