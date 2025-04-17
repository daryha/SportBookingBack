using BookingSports.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSports.Services;

namespace BookingSports.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Получить все отзывы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // Получить отзывы для тренера
        [HttpGet("coach/{coachId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForCoach(string coachId)
        {
            var reviews = await _reviewService.GetReviewsForCoachAsync(coachId);
            if (reviews == null)
            {
                return NotFound(new { message = "Отзывы не найдены" });
            }
            return Ok(reviews);
        }

        // Получить отзывы для спортивной площадки
        [HttpGet("facility/{facilityId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForFacility(string facilityId)
        {
            var reviews = await _reviewService.GetReviewsForFacilityAsync(facilityId);
            if (reviews == null)
            {
                return NotFound(new { message = "Отзывы не найдены" });
            }
            return Ok(reviews);
        }

        // Создать новый отзыв
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review model)
        {
            if (model.Score < 1 || model.Score > 5)
            {
                return BadRequest(new { message = "Оценка должна быть от 1 до 5" });
            }

            var createdReview = await _reviewService.CreateReviewAsync(model);
            return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
        }

        // Получить отзыв по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById(string id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // Обновить отзыв
        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateReview(string id, [FromBody] Review model)
        {
            var updatedReview = await _reviewService.UpdateReviewAsync(id, model);
            if (updatedReview == null)
            {
                return NotFound();
            }
            return Ok(updatedReview);
        }

        // Удалить отзыв
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(string id)
        {
            var success = await _reviewService.DeleteReviewAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
