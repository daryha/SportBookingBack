// Services/ICoachService.cs
using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface ICoachService
    {
        Task<IEnumerable<Coach>> GetAllCoachesAsync();
        Task<Coach?> GetCoachByIdAsync(string id);
        Task<Coach> CreateCoachAsync(Coach coach);
        Task<Coach?> UpdateCoachAsync(string id, Coach coach);
        Task<bool> DeleteCoachAsync(string id);

        // Фильтрация
        Task<IEnumerable<Coach>> GetFilteredCoachesAsync(
            decimal? minPrice,
            decimal? maxPrice,
            string? sportType
        );
    }
}
