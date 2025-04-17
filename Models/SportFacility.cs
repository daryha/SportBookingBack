using BookingSports.Models;

namespace BookingSports.Models
{
    public class SportFacility
    {
        public string Id { get; set; }
        public string Name { get; set; }        // Название
        public string Photo { get; set; }
        public string Address { get; set; }     // Адрес
        public string PhotoUrl { get; set; }    // Фото
        public string Description { get; set; } // Описание
        public decimal Price { get; set; }      // Цена за 2 часа

        // График работы (список доступных дат и времени)
        public List<Schedule> Schedule { get; set; } = new();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();


        // Оценки площадки
        public List<Review> Reviews { get; set; } = new();
    }
}
