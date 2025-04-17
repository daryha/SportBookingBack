using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Services
{
    public interface ICoachService
    {
        Task<Coach> CreateCoachAsync(Coach coach);
        Task<bool> DeleteCoachAsync(string id);
        Task<IEnumerable<Coach>> GetAllCoachesAsync();
        Task<Coach?> GetCoachByIdAsync(string id);
        Task<Coach> UpdateCoachAsync(string id, Coach coach);
    }


    public class CoachService : ICoachService
    {
        private readonly ApplicationDbContext _context;

        public CoachService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Coach> CreateCoachAsync(Coach coach)
        {
            coach.Id = Guid.NewGuid().ToString();
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();
            return coach;
        }

        public async Task<bool> DeleteCoachAsync(string id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return false;

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
        {
            return await _context.Coaches.ToListAsync();
        }

        public async Task<Coach?> GetCoachByIdAsync(string id)
        {
            return await _context.Coaches.FindAsync(id);
        }

        public async Task<Coach> UpdateCoachAsync(string id, Coach coach)
        {
            var existing = await _context.Coaches.FindAsync(id);
            if (existing == null) return null;

            existing.FirstName = coach.FirstName;
            existing.LastName = coach.LastName;
            existing.Title = coach.Title;
            existing.Description = coach.Description;
            existing.Experience = coach.Experience;
            existing.Price = coach.Price;
            existing.SportType = coach.SportType;
            existing.Photo = coach.Photo;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
