using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Services
{
    public interface ISportFacilityService
    {
        Task<SportFacility> CreateFacilityAsync(SportFacility facility);
        Task<bool> DeleteFacilityAsync(string id);
        Task<IEnumerable<SportFacility>> GetAllFacilitiesAsync();
        Task<SportFacility?> GetFacilityByIdAsync(string id);
        Task<SportFacility> UpdateFacilityAsync(string id, SportFacility facility);
    }


    public class SportFacilityService : ISportFacilityService
    {
        private readonly ApplicationDbContext _context;

        public SportFacilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SportFacility> CreateFacilityAsync(SportFacility facility)
        {
            facility.Id = Guid.NewGuid().ToString();
            _context.SportFacilities.Add(facility);
            await _context.SaveChangesAsync();
            return facility;
        }

        public async Task<bool> DeleteFacilityAsync(string id)
        {
            var facility = await _context.SportFacilities.FindAsync(id);
            if (facility == null) return false;

            _context.SportFacilities.Remove(facility);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SportFacility>> GetAllFacilitiesAsync()
        {
            return await _context.SportFacilities.ToListAsync();
        }

        public async Task<SportFacility?> GetFacilityByIdAsync(string id)
        {
            return await _context.SportFacilities.FindAsync(id);
        }

        public async Task<SportFacility> UpdateFacilityAsync(string id, SportFacility facility)
        {
            var existing = await _context.SportFacilities.FindAsync(id);
            if (existing == null) return null;

            existing.Name = facility.Name;
            existing.Description = facility.Description;
            existing.Address = facility.Address;
            existing.Price = facility.Price;
            existing.Schedule = facility.Schedule;
            existing.Photo = facility.Photo;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
