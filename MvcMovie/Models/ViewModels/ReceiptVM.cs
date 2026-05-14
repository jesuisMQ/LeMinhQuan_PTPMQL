using MvcMovie.Models.Baithuchanh12;
using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models.ViewModels;
public class ReceiptVM
{
    public string SupplierID { get; set; }=default!;
    public List<ReceiptDetails> Goods { get; set; }= new();
     public DateTime TimeStamp { get; set; }
     public int Total { get; set; }
}