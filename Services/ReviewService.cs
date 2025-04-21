using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _db;
        public ReviewService(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Review>> GetAllReviewsAsync() =>
            await _db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Coach)
                     .Include(r => r.SportFacility)
                     .ToListAsync();

        public async Task<IEnumerable<Review>> GetReviewsForCoachAsync(string coachId) =>
            await _db.Reviews
                     .Where(r => r.CoachId == coachId)
                     .Include(r => r.User)
                     .ToListAsync();

        public async Task<IEnumerable<Review>> GetReviewsForFacilityAsync(string facilityId) =>
            await _db.Reviews
                     .Where(r => r.SportFacilityId == facilityId)
                     .Include(r => r.User)
                     .ToListAsync();

        public async Task<Review?> GetReviewByIdAsync(string id) =>
            await _db.Reviews
                     .Include(r => r.User)
                     .Include(r => r.Coach)
                     .Include(r => r.SportFacility)
                     .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Review> CreateReviewAsync(Review review)
        {
            review.Id        = Guid.NewGuid().ToString();
            review.CreatedAt = DateTime.UtcNow;    // <— фиксируем время создания
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateReviewAsync(string id, Review review)
        {
            var existing = await _db.Reviews.FindAsync(id);
            if (existing == null) return null;
            existing.Score   = review.Score;
            existing.Comment = review.Comment;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteReviewAsync(string id)
        {
            var existing = await _db.Reviews.FindAsync(id);
            if (existing == null) return false;
            _db.Reviews.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
