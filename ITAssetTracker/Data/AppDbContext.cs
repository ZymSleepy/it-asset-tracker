using Microsoft.EntityFrameworkCore;
using ITAssetTracker.Models;

namespace ITAssetTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}