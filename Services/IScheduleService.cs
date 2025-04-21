// Services/IScheduleService.cs
using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule?>            GetScheduleByIdAsync(string id);
        Task<Schedule>             CreateScheduleAsync(Schedule schedule);
        Task<Schedule?>            UpdateScheduleAsync(string id, Schedule schedule);
        Task<bool>                 DeleteScheduleAsync(string id);
    }
}
