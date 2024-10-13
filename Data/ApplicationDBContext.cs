using InfluencersPlatformBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace InfluencersPlatformBackend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) :base(dbContextOptions) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InfluencerProfile> InfluencerProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one relationship between User and InfluencerProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.InfluencerProfile)
                .WithOne(p => p.User)
                .HasForeignKey<InfluencerProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete

            // One-to-one relationship between User and CompanyProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.CompanyProfile)
                .WithOne(p => p.User)
                .HasForeignKey<CompanyProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete
        }

    }
}
