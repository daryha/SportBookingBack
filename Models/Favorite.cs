using BookingSports.Models;

namespace BookingSports.Models
{
    public class Favorite
    {
        public string Id { get; set; }

        public string UserId { get; set; } // ID ������������
        public User User { get; set; }

        public string? CoachId { get; set; } // ID ������� (���� � ��������� ������)
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // ID �������� (���� � ��������� ��������)
        public SportFacility SportFacility { get; set; }
    }
}
