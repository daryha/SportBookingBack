using System;

namespace BookingSports.Models
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; } = null!;
        public User?  User   { get; set; }   // ← nullable

        public string? CoachId { get; set; }
        public Coach?  Coach   { get; set; } // ← nullable

        public string? SportFacilityId { get; set; }
        public SportFacility? SportFacility { get; set; } // ← nullable

        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime  { get; set; }
        public TimeSpan EndTime    { get; set; }
    }
}
