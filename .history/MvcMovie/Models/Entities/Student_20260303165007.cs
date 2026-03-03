using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public string StudentCode { get; set; }
        public string FullName { get; set; }
    }
}
