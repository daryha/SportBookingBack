using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(string id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> UpdateBookingAsync(string id, Booking booking);
        Task<bool> DeleteBookingAsync(string id);
        Task<Booking> GetBookingByFacilityAndTimeAsync(string SportFacilityId, DateTime BookingDate);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> GetBookingByIdAsync(string id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> UpdateBookingAsync(string id, Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking != null)
            {
                existingBooking.UserId = booking.UserId;
                existingBooking.CoachId = booking.CoachId;
                existingBooking.SportFacilityId = booking.SportFacilityId;
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.StartTime = booking.StartTime;
                existingBooking.EndTime = booking.EndTime;

                await _context.SaveChangesAsync();
            }

            return existingBooking;
        }

        public async Task<bool> DeleteBookingAsync(string id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Booking> GetBookingByFacilityAndTimeAsync(string SportFacilityId, DateTime BookingDate)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.SportFacilityId == SportFacilityId && b.BookingDate == BookingDate);
        }
    }
}
