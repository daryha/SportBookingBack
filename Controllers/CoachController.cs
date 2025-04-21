// Controllers/CoachController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;

namespace BookingSports.Controllers
{
    [Authorize(Roles = "Coach")]
    [Route("api/coach")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _svc;

        public CoachController(ICoachService svc)
        {
            _svc = svc;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        // PUT: api/coach/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCoachInfo([FromBody] Coach model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // предполагаем, что Coach.Id == userId
            var updated = await _svc.UpdateCoachAsync(userId!, model);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // GET: api/coach/{coachId}/schedule/excel
        [HttpGet("{coachId}/schedule/excel")]
        public async Task<IActionResult> ExportCoachScheduleToExcel(string coachId)
        {
            var coach = await _svc.GetCoachByIdAsync(coachId);
            if (coach == null) return NotFound();

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Schedule");

            // Header
            ws.Cells[1,1].Value = "Coach Name";
            ws.Cells[1,2].Value = "Facility";
            ws.Cells[1,3].Value = "Date";
            ws.Cells[1,4].Value = "Start";
            ws.Cells[1,5].Value = "End";

            int row = 2;
            foreach (var s in coach.Schedules)
            {
                ws.Cells[row,1].Value = coach.FirstName + " " + coach.LastName;
                ws.Cells[row,2].Value = s.SportFacility?.Name;
                ws.Cells[row,3].Value = s.Date.ToString("yyyy-MM-dd");
                ws.Cells[row,4].Value = s.StartTime.ToString(@"hh\:mm");
                ws.Cells[row,5].Value = s.EndTime.ToString(@"hh\:mm");
                row++;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var bytes = package.GetAsByteArray();
            var name  = $"{coach.FirstName}_{coach.LastName}_schedule.xlsx";
            return File(bytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        name);
        }
    }
}
