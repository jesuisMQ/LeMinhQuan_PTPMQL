using Microsoft.EntityFrameworkCore;
using MvcMovie.Models.Baithuchanh12;
using MvcMovie.Models.Baithuchanh13;
using MvcMovie.Models.Baithuchanh9;
using MvcMovie.Models.Entities;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students {get;set;}
        public DbSet<Quan> Quan {get;set;}
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<KhachHang> Customer { get; set; }
        public DbSet<DonHang> Orders { get; set; }
        public DbSet<ChitietDH> Details { get; set; }
        public DbSet<SanPham> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<ReceiptDetails> ReceiptDetails { get; set; }
        public DbSet<IssueNote> IssueNotes { get; set; }
         public DbSet<IssueNoteDetails> IssueNoteDetails { get; set; }
        public DbSet<MvcMovie.Models.Baithuchanh12.IssueNote> IssueNote { get; set; } = default!;
        public DbSet<Book> Books { get; set; }
    }
}
