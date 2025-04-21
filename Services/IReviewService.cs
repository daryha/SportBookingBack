// Services/IReviewService.cs
using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>>      GetAllReviewsAsync();
        Task<IEnumerable<Review>>      GetReviewsForCoachAsync(string coachId);
        Task<IEnumerable<Review>>      GetReviewsForFacilityAsync(string facilityId);
        Task<Review>                   CreateReviewAsync(Review review);
        Task<Review?>                  GetReviewByIdAsync(string id);
        Task<Review?>                  UpdateReviewAsync(string id, Review review);
        Task<bool>                     DeleteReviewAsync(string id);
    }
}
