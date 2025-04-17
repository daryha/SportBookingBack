using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<SportFacility> SportFacilities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

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
           .HasOne(s => s.SportFacility)  // Schedule ����� ���� SportFacility
           .WithMany()  // SportFacility ����� ����� ��������� Schedule
           .HasForeignKey(s => s.SportFacilityId)  // ������� ���� �� SportFacility
           .OnDelete(DeleteBehavior.Cascade);  // ��������� ��� �������� (�����������)
        }
    }
}
