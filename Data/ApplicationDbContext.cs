// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookingSports.Models;

namespace BookingSports.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coach>         Coaches         { get; set; }
        public DbSet<SportFacility> SportFacilities { get; set; }
        public DbSet<Schedule>      Schedules       { get; set; }
        public DbSet<Booking>       Bookings        { get; set; }
        public DbSet<Review>        Reviews         { get; set; }
        public DbSet<Favorite>      Favorites       { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.CoachId, b.BookingDate, b.StartTime })
                .IsUnique();
            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.SportFacilityId, b.BookingDate, b.StartTime })
                .IsUnique();
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.CoachId })
                .IsUnique();
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.SportFacilityId })
                .IsUnique();
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.CoachId })
                .IsUnique();
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.SportFacilityId })
                .IsUnique();

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.SportFacility)
                .WithMany(f => f.Schedules)
                .HasForeignKey(s => s.SportFacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Coach)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CoachId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
