using Microsoft.EntityFrameworkCore;

namespace Tweeter.Services.Data
{
    public class TweeterContext : DbContext
    {
        public TweeterContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TweeterContext).Assembly);
        }
    }
}