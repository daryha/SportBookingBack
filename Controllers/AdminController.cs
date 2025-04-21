using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSports.Controllers
{
    [Route("api/admin")]
    [ApiController]
    // Указываем, что авторизация идёт только по JWT‑Bearer и только для роли Admin
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin"
    )]
    public class AdminController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private readonly ISportFacilityService _facilityService;

        public AdminController(
            ICoachService coachService,
            ISportFacilityService facilityService)
        {
            _coachService    = coachService;
            _facilityService = facilityService;
        }

        // ================= COACH =================

        // POST /api/admin/coaches
        [HttpPost("coaches")]
        public async Task<IActionResult> CreateCoach([FromBody] Coach coach)
        {
            var created = await _coachService.CreateCoachAsync(coach);
            return CreatedAtAction(
                nameof(GetCoachById),
                new { id = created.Id },
                created
            );
        }

        // GET /api/admin/coaches
        [HttpGet("coaches")]
        public async Task<IActionResult> GetAllCoaches()
        {
            var list = await _coachService.GetAllCoachesAsync();
            return Ok(list);
        }

        // GET /api/admin/coaches/{id}
        [HttpGet("coaches/{id}")]
        public async Task<IActionResult> GetCoachById(string id)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        // PUT /api/admin/coaches/{id}
        [HttpPut("coaches/{id}")]
        public async Task<IActionResult> UpdateCoach(
            string id,
            [FromBody] Coach coach)
        {
            var updated = await _coachService.UpdateCoachAsync(id, coach);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE /api/admin/coaches/{id}
        [HttpDelete("coaches/{id}")]
        public async Task<IActionResult> DeleteCoach(string id)
        {
            var ok = await _coachService.DeleteCoachAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }


        // ============== FACILITY ==============

        // POST /api/admin/facilities
        [HttpPost("facilities")]
        public async Task<IActionResult> CreateFacility(
            [FromBody] SportFacility facility)
        {
            var created = await _facilityService.CreateFacilityAsync(facility);
            return CreatedAtAction(
                nameof(GetFacilityById),
                new { id = created.Id },
                created
            );
        }

        // GET /api/admin/facilities
        [HttpGet("facilities")]
        public async Task<IActionResult> GetAllFacilities()
        {
            var list = await _facilityService.GetAllFacilitiesAsync();
            return Ok(list);
        }

        // GET /api/admin/facilities/{id}
        [HttpGet("facilities/{id}")]
        public async Task<IActionResult> GetFacilityById(string id)
        {
            var f = await _facilityService.GetFacilityByIdAsync(id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        // PUT /api/admin/facilities/{id}
        [HttpPut("facilities/{id}")]
        public async Task<IActionResult> UpdateFacility(
            string id,
            [FromBody] SportFacility facility)
        {
            var updated = await _facilityService.UpdateFacilityAsync(id, facility);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE /api/admin/facilities/{id}
        [HttpDelete("facilities/{id}")]
        public async Task<IActionResult> DeleteFacility(string id)
        {
            var ok = await _facilityService.DeleteFacilityAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
