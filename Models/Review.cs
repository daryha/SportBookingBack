using BookingSports.Models;

namespace BookingSports.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string UserId { get; set; } // ID пользователя, который оставил отзыв
        public User User { get; set; }

        public string? CoachId { get; set; } // ID тренера (если оценивают тренера)
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // ID площадки (если оценивают площадку)
        public SportFacility SportFacility { get; set; }

        public int Score { get; set; } // Оценка (1-5)
        public string Comment { get; set; } // Комментарий
    }
}