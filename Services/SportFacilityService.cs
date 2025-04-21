using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class SportFacilityService : ISportFacilityService
    {
        private readonly ApplicationDbContext _db;
        public SportFacilityService(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<SportFacility>> GetAllFacilitiesAsync() =>
            await _db.SportFacilities
                     .Include(f => f.Schedules)
                     .Include(f => f.Reviews)
                     .ToListAsync();

        public async Task<SportFacility?> GetFacilityByIdAsync(string id) =>
            await _db.SportFacilities
                     .Include(f => f.Schedules)
                     .Include(f => f.Reviews)
                     .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<SportFacility> CreateFacilityAsync(SportFacility facility)
        {
            facility.Id = Guid.NewGuid().ToString();
            _db.SportFacilities.Add(facility);
            await _db.SaveChangesAsync();
            return facility;
        }

        public async Task<SportFacility?> UpdateFacilityAsync(string id, SportFacility facility)
        {
            var existing = await _db.SportFacilities.FindAsync(id);
            if (existing == null) return null;

            // Обновляем все поля
            existing.Name           = facility.Name;
            existing.Address        = facility.Address;
            existing.PhotoUrl       = facility.PhotoUrl;
            existing.Description    = facility.Description;
            existing.Price          = facility.Price;
            existing.HasLockerRooms = facility.HasLockerRooms;
            existing.HasStands      = facility.HasStands;
            existing.HasShower      = facility.HasShower;
            existing.HasLighting    = facility.HasLighting;
            existing.HasParking     = facility.HasParking;
            existing.HasEquipment   = facility.HasEquipment;
            existing.Capacity       = facility.Capacity;
            existing.Length         = facility.Length;
            existing.Width          = facility.Width;
            existing.Height         = facility.Height;
            existing.SurfaceType    = facility.SurfaceType;
            existing.SportTypes     = facility.SportTypes;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteFacilityAsync(string id)
        {
            // — сначала удаляем все Favorites и Reviews, чтобы не было FK-ошибок
            var favs    = _db.Favorites.Where(f => f.SportFacilityId == id);
            var reviews = _db.Reviews.Where(r => r.SportFacilityId   == id);
            _db.Favorites.RemoveRange(favs);
            _db.Reviews   .RemoveRange(reviews);

            var facility = await _db.SportFacilities.FindAsync(id);
            if (facility == null) return false;

            _db.SportFacilities.Remove(facility);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SportFacility>> GetFilteredFacilitiesAsync(
            string? city,
            decimal? minPrice,
            decimal? maxPrice,
            string? sportType
        )
        {
            var query = _db.SportFacilities
                           .Include(f => f.Schedules)
                           .Include(f => f.Reviews)
                           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(f => f.Address.Contains(city));

            if (minPrice.HasValue)
                query = query.Where(f => f.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(f => f.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(sportType))
                query = query.Where(f => f.SportTypes.Contains(sportType));

            return await query.ToListAsync();
        }
    }
}
