using BookingSports.Models;

namespace BookingSports.Models
{
    public class Favorite
    {
        public string Id { get; set; }

        public string UserId { get; set; } // ID пользователя
        public User User { get; set; }

        public string? CoachId { get; set; } // ID тренера (если в избранном тренер)
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // ID площадки (если в избранном площадка)
        public SportFacility SportFacility { get; set; }
    }
}
