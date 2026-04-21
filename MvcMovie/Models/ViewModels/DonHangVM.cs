using MvcMovie.Models.Baithuchanh9;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.ViewModels;
public class DonHangVM
{

    public string OrderID { get; set; }=default!;
    public string CustomerID { get; set; }=default!;
    public DateTime Timestamp { get; set; }=default!;
    public List<ChitietDH> Products { get; set; }= new();
    [DisplayFormat(DataFormatString = "{0:#,##0}")]
    public decimal SumAll { get; set; }=default!;

}