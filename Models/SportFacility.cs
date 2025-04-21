// Models/SportFacility.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingSports.Models
{
    public class SportFacility
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // базовые поля
        public string Name        { get; set; }
        public string PhotoUrl    { get; set; }    // оставляем только URL
        public string Address     { get; set; }
        public string Description { get; set; }
        public decimal Price      { get; set; }

        // удобства
        public bool HasLockerRooms { get; set; }
        public bool HasStands      { get; set; }
        public bool HasShower      { get; set; }
        public bool HasLighting    { get; set; }
        public bool HasParking     { get; set; }
        public bool HasEquipment   { get; set; }

        // вместимость и размеры
        public int Capacity    { get; set; }
        public decimal Length  { get; set; }
        public decimal Width   { get; set; }
        public decimal Height  { get; set; }

        // покрытие
        public string SurfaceType { get; set; }

        // типы спорта (массив строк)
        [Column(TypeName = "text[]")]
        public string[] SportTypes { get; set; } = Array.Empty<string>();

        // навигационные свойства — не включаем в JSON
        [JsonIgnore] public List<Schedule> Schedules { get; set; } = new();
        [JsonIgnore] public List<Review>   Reviews   { get; set; } = new();
    }
}
