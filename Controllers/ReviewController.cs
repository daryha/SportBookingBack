// Controllers/ReviewController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _svc;
        public ReviewController(IReviewService svc) => _svc = svc;

        // GET /api/review/coach/{coachId}
        [HttpGet("coach/{coachId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForCoach(string coachId)
        {
            var list = await _svc.GetReviewsForCoachAsync(coachId);
            return Ok(list.Select(ToDto));
        }

        // GET /api/review/facility/{facilityId}
        [HttpGet("facility/{facilityId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForFacility(string facilityId)
        {
            var list = await _svc.GetReviewsForFacilityAsync(facilityId);
            return Ok(list.Select(ToDto));
        }

        // POST /api/review/coach/{coachId}
        [HttpPost("coach/{coachId}")]
        public async Task<ActionResult<ReviewDto>> CreateForCoach(string coachId, [FromBody] Review model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            model.Id                = Guid.NewGuid().ToString();
            model.UserId            = userId;
            model.CoachId           = coachId;
            model.SportFacilityId   = null;

            if (model.Score < 1 || model.Score > 5)
                return BadRequest(new { message = "Score must be between 1 and 5." });

            try
            {
                await _svc.CreateReviewAsync(model);
                // повторно читаем с Includes
                var full = await _svc.GetReviewByIdAsync(model.Id);
                return CreatedAtAction(nameof(GetForCoach),
                    new { coachId },
                    ToDto(full!));
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
            {
                return Conflict(new { message = "Вы уже оставили отзыв для этого тренера." });
            }
        }

        // POST /api/review/facility/{facilityId}
        [HttpPost("facility/{facilityId}")]
        public async Task<ActionResult<ReviewDto>> CreateForFacility(string facilityId, [FromBody] Review model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            model.Id                = Guid.NewGuid().ToString();
            model.UserId            = userId;
            model.SportFacilityId   = facilityId;
            model.CoachId           = null;

            if (model.Score < 1 || model.Score > 5)
                return BadRequest(new { message = "Score must be between 1 and 5." });

            try
            {
                await _svc.CreateReviewAsync(model);
                var full = await _svc.GetReviewByIdAsync(model.Id);
                return CreatedAtAction(nameof(GetForFacility),
                    new { facilityId },
                    ToDto(full!));
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
            {
                return Conflict(new { message = "Вы уже оставили отзыв для этой площадки." });
            }
        }

        // PUT /api/review/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDto>> Update(string id, [FromBody] Review model)
        {
            var updated = await _svc.UpdateReviewAsync(id, model);
            if (updated == null) return NotFound();

            // снова подгружаем навигацию
            var full = await _svc.GetReviewByIdAsync(id);
            return Ok(ToDto(full!));
        }

        // DELETE /api/review/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            await _svc.DeleteReviewAsync(id) ? NoContent() : NotFound();

        // Маппинг Review → ReviewDto
        private static ReviewDto ToDto(Review r) => new()
        {
            Id             = r.Id,
            UserId         = r.UserId,
            UserName       = $"{r.User?.FirstName ?? ""} {r.User?.LastName ?? ""}".Trim(),
            CreatedAt      = r.CreatedAt,
            Score          = r.Score,
            Comment        = r.Comment,
            CoachId        = r.CoachId,
            CoachName      = r.Coach is not null
                                ? $"{r.Coach.FirstName} {r.Coach.LastName}"
                                : null,
            FacilityId     = r.SportFacilityId,
            FacilityName   = r.SportFacility?.Name
        };
    }
}
