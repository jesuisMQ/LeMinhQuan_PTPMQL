using MvcMovie.Models.Baithuchanh9;
namespace MvcMovie.Models.ViewModels;
public class CustomerFullVM
{
    public string CustomerID { get; set; }

    public KhachHang Customer { get; set; }
}