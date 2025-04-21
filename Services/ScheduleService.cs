// Services/ScheduleService.cs
using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _db;
        public ScheduleService(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync() =>
            await _db.Schedules
                     .Include(s => s.Coach)
                     .Include(s => s.SportFacility)
                     .ToListAsync();

        public async Task<Schedule?> GetScheduleByIdAsync(string id) =>
            await _db.Schedules
                     .Include(s => s.Coach)
                     .Include(s => s.SportFacility)
                     .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            schedule.Id = Guid.NewGuid().ToString();
            _db.Schedules.Add(schedule);
            await _db.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule?> UpdateScheduleAsync(string id, Schedule schedule)
        {
            var existing = await _db.Schedules.FindAsync(id);
            if (existing == null) return null;

            existing.Date             = schedule.Date;
            existing.StartTime        = schedule.StartTime;
            existing.EndTime          = schedule.EndTime;
            existing.CoachId          = schedule.CoachId;
            existing.SportFacilityId  = schedule.SportFacilityId;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteScheduleAsync(string id)
        {
            var existing = await _db.Schedules.FindAsync(id);
            if (existing == null) return false;
            _db.Schedules.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
