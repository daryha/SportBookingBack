using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using BookingSports.Models;

namespace BookingSports.Models
{
    public class User : IdentityUser

    {
        public string FirstName { get; set; }  // Имя
        public string LastName { get; set; }   // Фамилия
        public string City { get; set; }       // Город проживания

        // Избранное - список избранных тренеров и площадок
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        // Связь с бронированиями пользователя
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();  // добавление связи с бронированиями
    }
}
