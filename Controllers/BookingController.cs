// Controllers/BookingController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/booking")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ICoachService   _coachService;

        public BookingController(IBookingService bookingService, ICoachService coachService)
        {
            _bookingService = bookingService;
            _coachService   = coachService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings() =>
            Ok(await _bookingService.GetAllBookingsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(string id)
        {
            var b = await _bookingService.GetBookingByIdAsync(id);
            return b == null ? NotFound() : Ok(b);
        }

        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetMyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            var mine = await _bookingService.GetBookingsForUserAsync(userId);
            return Ok(mine);
        }

        [HttpPost("facility")]
        public async Task<IActionResult> CreateFacilityBooking([FromBody] Booking model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            model.UserId = userId;
            if (string.IsNullOrEmpty(model.SportFacilityId))
                return BadRequest(new { message = "sportFacilityId обязателен" });

            model.BookingDate = DateTime.SpecifyKind(model.BookingDate, DateTimeKind.Utc);

            var conflict = await _bookingService.GetBookingByFacilityAndTimeAsync(
                model.SportFacilityId, model.BookingDate);
            if (conflict != null)
                return Conflict(new { message = "Это время уже занято." });

            var created = await _bookingService.CreateBookingAsync(model);
            var full    = await _bookingService.GetBookingByIdAsync(created.Id);
            return CreatedAtAction(nameof(GetBookingById), new { id = full!.Id }, full);
        }

        [HttpPost("coach")]
        public async Task<IActionResult> CreateCoachBooking([FromBody] Booking model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            model.UserId = userId;
            if (string.IsNullOrEmpty(model.CoachId))
                return BadRequest(new { message = "coachId обязателен" });

            model.BookingDate = DateTime.SpecifyKind(model.BookingDate, DateTimeKind.Utc);

            var conflict = await _bookingService.GetBookingByCoachAndTimeAsync(
                model.CoachId, model.BookingDate, model.StartTime);
            if (conflict != null)
                return Conflict(new { message = "Это время уже занято у этого тренера." });

            var created = await _bookingService.CreateBookingAsync(model);
            var full    = await _bookingService.GetBookingByIdAsync(created.Id);

            var coach = await _coachService.GetCoachByIdAsync(model.CoachId);
            if (coach == null)
                return CreatedAtAction(nameof(GetBookingById), new { id = full!.Id }, full);

            return CreatedAtAction(
                nameof(GetBookingById),
                new { id = full!.Id },
                new {
                  booking       = full,
                  coachContacts = new {
                    coach.Phone,
                    coach.Telegram,
                    coach.WhatsApp
                  }
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> UpdateBooking(
            string id,
            [FromBody] Booking model)
        {
            var updated = await _bookingService.UpdateBookingAsync(id, model);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id) =>
            await _bookingService.DeleteBookingAsync(id)
              ? NoContent()
              : NotFound();
    }
}
