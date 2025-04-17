using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSports.Data;

namespace BookingSports.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(string id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            var existingBooking = await _bookingService.GetBookingByFacilityAndTimeAsync(booking.SportFacilityId, booking.BookingDate);
            if (existingBooking != null)
            {
                return Conflict(new { message = "Ѕронирование уже существует дл€ данного времени." });
            }

            var createdBooking = await _bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.Id }, createdBooking);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> UpdateBooking(string id, Booking booking)
        {
            var updatedBooking = await _bookingService.UpdateBookingAsync(id, booking);
            if (updatedBooking == null)
            {
                return NotFound();
            }
            return Ok(updatedBooking);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(string id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
