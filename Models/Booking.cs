using BookingSports.Models;

namespace BookingSports.Models
{
    public class Booking
    {
        public string Id { get; set; } // »дентификатор бронировани€
        public string UserId { get; set; } // ID пользовател€, который забронировал
        public User User { get; set; }

        public string? CoachId { get; set; } // ID тренера (если бронируетс€ тренер)
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // ID площадки (если бронируетс€ площадка)
        public SportFacility SportFacility { get; set; }

        public DateTime BookingDate { get; set; } // ƒата бронировани€
        public TimeSpan StartTime { get; set; }   // ¬рем€ начала
        public TimeSpan EndTime { get; set; }     // ¬рем€ окончани€
    }
}

