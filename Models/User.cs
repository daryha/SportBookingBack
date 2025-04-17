using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using BookingSports.Models;

namespace BookingSports.Models
{
    public class User : IdentityUser

    {
        public string FirstName { get; set; }  // ���
        public string LastName { get; set; }   // �������
        public string City { get; set; }       // ����� ����������

        // ��������� - ������ ��������� �������� � ��������
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        // ����� � �������������� ������������
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();  // ���������� ����� � ��������������
    }
}
