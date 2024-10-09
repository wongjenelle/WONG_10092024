using Microsoft.EntityFrameworkCore;
using UpStreamer.Server.Database.Entities;

namespace UpStreamer.Server.Database
{
    public class UpStreamerDbContext(DbContextOptions<UpStreamerDbContext> options) : DbContext(options)
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Video> Categories { get; set; }
    }
}
