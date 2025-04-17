using BookingSports.Models;

namespace BookingSports.Models
{
    public class SportFacility
    {
        public string Id { get; set; }
        public string Name { get; set; }        // ��������
        public string Photo { get; set; }
        public string Address { get; set; }     // �����
        public string PhotoUrl { get; set; }    // ����
        public string Description { get; set; } // ��������
        public decimal Price { get; set; }      // ���� �� 2 ����

        // ������ ������ (������ ��������� ��� � �������)
        public List<Schedule> Schedule { get; set; } = new();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();


        // ������ ��������
        public List<Review> Reviews { get; set; } = new();
    }
}
