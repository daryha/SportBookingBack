// Services/CoachService.cs
using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class CoachService : ICoachService
    {
        private readonly ApplicationDbContext _db;
        public CoachService(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Coach>> GetAllCoachesAsync() =>
            await _db.Coaches.ToListAsync();

        public async Task<Coach?> GetCoachByIdAsync(string id) =>
            await _db.Coaches.FindAsync(id);

        public async Task<Coach> CreateCoachAsync(Coach coach)
        {
            coach.Id = Guid.NewGuid().ToString();
            _db.Coaches.Add(coach);
            await _db.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach?> UpdateCoachAsync(string id, Coach coach)
        {
            var existing = await _db.Coaches.FindAsync(id);
            if (existing == null) return null;

            existing.FirstName   = coach.FirstName;
            existing.LastName    = coach.LastName;
            existing.Title       = coach.Title;
            existing.Description = coach.Description;
            existing.Experience  = coach.Experience;
            existing.Price       = coach.Price;
            existing.SportType   = coach.SportType;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteCoachAsync(string id)
        {
            var c = await _db.Coaches.FindAsync(id);
            if (c == null) return false;
            _db.Coaches.Remove(c);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Coach>> GetFilteredCoachesAsync(
            decimal? minPrice,
            decimal? maxPrice,
            string? sportType
        )
        {
            var q = _db.Coaches.AsQueryable();

            if (minPrice.HasValue)
                q = q.Where(c => c.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                q = q.Where(c => c.Price <= maxPrice.Value);
            if (!string.IsNullOrWhiteSpace(sportType))
                q = q.Where(c => c.SportType == sportType);

            return await q.ToListAsync();
        }
    }
}
