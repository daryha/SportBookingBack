using BookingSports.Models;
using BookingSports.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(string id);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task<Schedule> UpdateScheduleAsync(string id, Schedule schedule);
        Task<bool> DeleteScheduleAsync(string id);
    }

    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить все расписания
        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        // Получить расписание по ID
        public async Task<Schedule> GetScheduleByIdAsync(string id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        // Создать новое расписание
        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        // Обновить расписание
        public async Task<Schedule> UpdateScheduleAsync(string id, Schedule schedule)
        {
            var existingSchedule = await _context.Schedules.FindAsync(id);
            if (existingSchedule == null)
            {
                return null;
            }

            existingSchedule.Date = schedule.Date;
            existingSchedule.StartTime = schedule.StartTime;
            existingSchedule.EndTime = schedule.EndTime;
            existingSchedule.SportFacilityId = schedule.SportFacilityId;

            _context.Schedules.Update(existingSchedule);
            await _context.SaveChangesAsync();
            return existingSchedule;
        }

        // Удалить расписание
        public async Task<bool> DeleteScheduleAsync(string id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return false;
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
