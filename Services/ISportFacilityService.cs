using BookingSports.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface ISportFacilityService
    {
        Task<IEnumerable<SportFacility>> GetAllFacilitiesAsync();
        Task<SportFacility?> GetFacilityByIdAsync(string id);
        Task<SportFacility> CreateFacilityAsync(SportFacility facility);
        Task<SportFacility?> UpdateFacilityAsync(string id, SportFacility facility);
        Task<bool> DeleteFacilityAsync(string id);

        // Новый метод для фильтрации
        Task<IEnumerable<SportFacility>> GetFilteredFacilitiesAsync(
            string? city,
            decimal? minPrice,
            decimal? maxPrice,
            string? sportType
        );
    }
}
