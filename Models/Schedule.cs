using BookingSports.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSports.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime AvailableDate { get; set; } // Доступная дата
        public TimeSpan StartTime { get; set; } // Время начала
        public TimeSpan EndTime { get; set; }   // Время окончания

        public string? CoachId { get; set; } // Если это тренер
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // Если это площадка
        [ForeignKey("SportFacilityId")]
        public SportFacility SportFacility { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
