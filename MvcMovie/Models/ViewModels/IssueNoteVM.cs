using MvcMovie.Models.Baithuchanh12;
using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models.ViewModels;
public class IssueNoteVM{
        public DateTime TimeStamp { get; set; }
        public string SupplierID { get; set; }=default!;
        public List<IssueNoteDetails>  Goods { get; set; }= new();
        public int Total { get; set; }
}