using System.Text.Json.Serialization;  // ← для [JsonIgnore]

namespace BookingSports.Models
{
    public class Favorite
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Сохраняем только ссылку
        public string UserId { get; set; } = null!;

        // Навигационный объект — игнорируем
        [JsonIgnore]
        public User? User { get; set; }

        public string? CoachId { get; set; }
        [JsonIgnore]
        public Coach? Coach { get; set; }

        public string? SportFacilityId { get; set; }
        [JsonIgnore]
        public SportFacility? SportFacility { get; set; }
    }
}
