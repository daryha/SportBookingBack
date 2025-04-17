using BookingSports.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSports.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime AvailableDate { get; set; } // ��������� ����
        public TimeSpan StartTime { get; set; } // ����� ������
        public TimeSpan EndTime { get; set; }   // ����� ���������

        public string? CoachId { get; set; } // ���� ��� ������
        public Coach Coach { get; set; }

        public string? SportFacilityId { get; set; } // ���� ��� ��������
        [ForeignKey("SportFacilityId")]
        public SportFacility SportFacility { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
