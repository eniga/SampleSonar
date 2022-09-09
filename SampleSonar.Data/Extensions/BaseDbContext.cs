using Microsoft.EntityFrameworkCore;
using SampleSonar.Data.Entities;

namespace SampleSonar.Data.Extensions
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<User> User { get; set; }
    }
}
