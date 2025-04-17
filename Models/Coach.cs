using BookingSports.Models;

namespace BookingSports.Models
{
    public class Coach
    {
        public string Id { get; set; }
        public string FirstName { get; set; }  // ���
        public string LastName { get; set; }   // �������
        public string Photo { get; set; }
        public string SportType { get; set; }  // ��� ������
        public string Title { get; set; }      // ������ (���, �� � �. �.)
        public int Experience { get; set; }    // ���� (���)
        public string PhotoUrl { get; set; }   // ����
        public string Description { get; set; } // ��������
        public decimal Price { get; set; }     // ���� �� 2 ����

        // ������ ������ (������ ��������� ���)
        public List<Schedule> Schedules { get; set; } = new();

        // ������ �������
        public List<Review> Reviews { get; set; } = new();
    }
}
