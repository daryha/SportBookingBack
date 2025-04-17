using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // Получить все расписания
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        // Получить расписание по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(string id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        // Создать новое расписание
        [HttpPost]
        public async Task<ActionResult<Schedule>> CreateSchedule(Schedule schedule)
        {
            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdSchedule.Id }, createdSchedule);
        }

        // Обновить расписание
        [HttpPut("{id}")]
        public async Task<ActionResult<Schedule>> UpdateSchedule(string id, Schedule schedule)
        {
            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(id, schedule);
            if (updatedSchedule == null)
            {
                return NotFound();
            }
            return Ok(updatedSchedule);
        }

        // Удалить расписание
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchedule(string id)
        {
            var success = await _scheduleService.DeleteScheduleAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
