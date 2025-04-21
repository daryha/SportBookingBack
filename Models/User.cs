// Models/User.cs
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookingSports.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName  { get; set; } = null!;
        public string City      { get; set; } = null!;

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Booking>  Bookings  { get; set; } = new List<Booking>();
        public ICollection<Review>   Reviews   { get; set; } = new List<Review>();
    }
}
