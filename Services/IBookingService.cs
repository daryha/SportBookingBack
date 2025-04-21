// Services/IBookingService.cs
using BookingSports.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(string id);
        Task<Booking?> GetBookingByFacilityAndTimeAsync(string? facilityId, DateTime bookingDate);
        Task<Booking?> GetBookingByCoachAndTimeAsync(string? coachId, DateTime bookingDate, TimeSpan startTime);
        Task<IEnumerable<Booking>> GetBookingsForUserAsync(string userId);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> UpdateBookingAsync(string id, Booking booking);
        Task<bool> DeleteBookingAsync(string id);
    }
}
