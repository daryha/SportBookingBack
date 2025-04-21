// Models/Coach.cs
using System;
using System.Collections.Generic;

namespace BookingSports.Models
{
    public class Coach
    {
        public string Id            { get; set; } = Guid.NewGuid().ToString();
        public string FirstName     { get; set; } = null!;
        public string LastName      { get; set; } = null!;
        public string SportType     { get; set; } = string.Empty;
        public string Title         { get; set; } = string.Empty;
        public int    Experience    { get; set; }
        public string PhotoUrl      { get; set; } = string.Empty;
        public string Description   { get; set; } = string.Empty;
        public decimal Price        { get; set; }

        public string Phone     { get; set; }
        public string Telegram  { get; set; }
        public string WhatsApp  { get; set; }

        public List<Schedule> Schedules { get; set; } = new();
        public List<Review>   Reviews   { get; set; } = new();
    }
}
