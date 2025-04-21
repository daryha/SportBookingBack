using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingSports.Models
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public string? CoachId { get; set; }
        [JsonIgnore]
        public Coach? Coach { get; set; }

        public string? SportFacilityId { get; set; }
        [JsonIgnore]
        public SportFacility? SportFacility { get; set; }

        public int Score { get; set; }
        public string Comment { get; set; } = "";

        public DateTime CreatedAt { get; set; }    // <— сюда записываем время
    }
}
