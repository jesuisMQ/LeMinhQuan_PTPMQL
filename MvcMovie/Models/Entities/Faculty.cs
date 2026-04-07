using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
namespace MvcMovie.Models.Entities
{
    public class Faculty 
    {
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        
    }
}