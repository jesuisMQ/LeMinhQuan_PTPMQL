namespace MvcMovie.Models.ViewModels
{
    public class StudentVM
    {
        public string StudentCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string FacultyName { get; set; } = default!;
        public string Email { get; set; }= default!;
    }
}