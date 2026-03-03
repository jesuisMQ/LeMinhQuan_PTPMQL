using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students {get;set;}
    }
}
