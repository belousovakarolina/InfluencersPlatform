using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace InfluencersPlatformBackend.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) :base(dbContextOptions) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; } //TODO: hmmm?
        public DbSet<InfluencerProfile> InfluencerProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one relationship between User and InfluencerProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.InfluencerProfile)
                .WithOne(p => p.User)
                .HasForeignKey<InfluencerProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete

            //TODO: ar mano useriai yra trinami? ar ju profiles yra trinami? kaip su komentaru istorija?
            // One-to-one relationship between User and CompanyProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.CompanyProfile)
                .WithOne(p => p.User)
                .HasForeignKey<CompanyProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete
        }

    }
}
