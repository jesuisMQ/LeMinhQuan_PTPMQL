using Microsoft.EntityFrameworkCore;
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
    }
}
