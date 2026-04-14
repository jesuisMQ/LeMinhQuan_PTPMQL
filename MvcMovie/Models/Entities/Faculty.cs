using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.Text;
namespace MvcMovie.Models.Entities

{
    public class Faculty 
    {
        [Key]
        [Required(ErrorMessage = "Ma khoa khong duoc de trong")]
        public string FacultyID { get; set; }
        [Required(ErrorMessage = "Ten khoa khong duoc de trong")]
        public string FacultyName { get; set; }
        public ICollection<Student>? Students{ get; set; }
        
    }
}