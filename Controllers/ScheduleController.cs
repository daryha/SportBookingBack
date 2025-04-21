// Controllers/ScheduleController.cs
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
        private readonly IScheduleService _svc;
        public ScheduleController(IScheduleService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll() =>
            Ok(await _svc.GetAllSchedulesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetById(string id)
        {
            var s = await _svc.GetScheduleByIdAsync(id);
            return s == null ? NotFound() : Ok(s);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> Create([FromBody] Schedule model)
        {
            var created = await _svc.CreateScheduleAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Schedule>> Update(string id, [FromBody] Schedule model)
        {
            var updated = await _svc.UpdateScheduleAsync(id, model);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            await _svc.DeleteScheduleAsync(id) ? NoContent() : NotFound();
    }
}
