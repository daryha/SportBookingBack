using BookingSports.Models;

namespace BookingSports.Models
{
    public class Coach
    {
        public string Id { get; set; }
        public string FirstName { get; set; }  // Имя
        public string LastName { get; set; }   // Фамилия
        public string Photo { get; set; }
        public string SportType { get; set; }  // Вид спорта
        public string Title { get; set; }      // Звание (КМС, МС и т. д.)
        public int Experience { get; set; }    // Стаж (лет)
        public string PhotoUrl { get; set; }   // Фото
        public string Description { get; set; } // Описание
        public decimal Price { get; set; }     // Цена за 2 часа

        // График работы (список доступных дат)
        public List<Schedule> Schedules { get; set; } = new();

        // Оценки тренера
        public List<Review> Reviews { get; set; } = new();
    }
}
