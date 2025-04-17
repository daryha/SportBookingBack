using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSports.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private readonly ISportFacilityService _facilityService;

        public AdminController(ICoachService coachService, ISportFacilityService facilityService)
        {
            _coachService = coachService;
            _facilityService = facilityService;
        }

        // === COACH ===

        [HttpPost("coaches")]
        public async Task<IActionResult> CreateCoach([FromBody] Coach coach)
        {
            var createdCoach = await _coachService.CreateCoachAsync(coach);
            return CreatedAtAction(nameof(GetCoachById), new { id = createdCoach.Id }, createdCoach);
        }

        [HttpDelete("coaches/{id}")]
        public async Task<IActionResult> DeleteCoach(string id)
        {
            var result = await _coachService.DeleteCoachAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("coaches")]
        public async Task<IActionResult> GetAllCoaches()
        {
            var coaches = await _coachService.GetAllCoachesAsync();
            return Ok(coaches);
        }

        [HttpGet("coaches/{id}")]
        public async Task<IActionResult> GetCoachById(string id)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        [HttpPut("coaches/{id}")]
        public async Task<IActionResult> UpdateCoach(string id, [FromBody] Coach coach)
        {
            var updated = await _coachService.UpdateCoachAsync(id, coach);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // === FACILITY ===

        [HttpPost("facilities")]
        public async Task<IActionResult> CreateFacility([FromBody] SportFacility facility)
        {
            var createdFacility = await _facilityService.CreateFacilityAsync(facility);
            return CreatedAtAction(nameof(GetFacilityById), new { id = createdFacility.Id }, createdFacility);
        }

        [HttpDelete("facilities/{id}")]
        public async Task<IActionResult> DeleteFacility(string id)
        {
            var result = await _facilityService.DeleteFacilityAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("facilities")]
        public async Task<IActionResult> GetAllFacilities()
        {
            var facilities = await _facilityService.GetAllFacilitiesAsync();
            return Ok(facilities);
        }

        [HttpGet("facilities/{id}")]
        public async Task<IActionResult> GetFacilityById(string id)
        {
            var facility = await _facilityService.GetFacilityByIdAsync(id);
            if (facility == null) return NotFound();
            return Ok(facility);
        }

        [HttpPut("facilities/{id}")]
        public async Task<IActionResult> UpdateFacility(string id, [FromBody] SportFacility facility)
        {
            var updated = await _facilityService.UpdateFacilityAsync(id, facility);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
