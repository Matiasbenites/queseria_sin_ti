using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }
}
