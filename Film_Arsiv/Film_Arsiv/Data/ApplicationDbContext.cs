using Microsoft.EntityFrameworkCore;
using Film_Arsiv.Models;

namespace Film_Arsiv.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Film> Films { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
