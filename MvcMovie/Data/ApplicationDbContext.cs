using Microsoft.EntityFrameworkCore;
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
    }
}
