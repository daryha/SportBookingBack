using BookingSports.Models;
using BookingSports.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<IEnumerable<Review>> GetReviewsForCoachAsync(string coachId);
        Task<IEnumerable<Review>> GetReviewsForFacilityAsync(string facilityId);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(string id);
        Task<Review> UpdateReviewAsync(string id, Review review);
        Task<bool> DeleteReviewAsync(string id);
    }

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.Include(r => r.User).Include(r => r.Coach).Include(r => r.SportFacility).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsForCoachAsync(string coachId)
        {
            return await _context.Reviews
                .Where(r => r.CoachId == coachId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsForFacilityAsync(string facilityId)
        {
            return await _context.Reviews
                .Where(r => r.SportFacilityId == facilityId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> GetReviewByIdAsync(string id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Coach)
                .Include(r => r.SportFacility)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> UpdateReviewAsync(string id, Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(id);
            if (existingReview == null)
                return null;

            existingReview.Score = review.Score;
            existingReview.Comment = review.Comment;

            await _context.SaveChangesAsync();
            return existingReview;
        }

        public async Task<bool> DeleteReviewAsync(string id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
