// Controllers/FacilityController.cs
using BookingSports.Data;
using BookingSports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BookingSports.Controllers
{
    [Authorize(Roles = "SportFacility")]
    [Route("api/facility")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public FacilityController(ApplicationDbContext db)
        {
            _db = db;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFacilityInfo([FromBody] SportFacility model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var facility = await _db.SportFacilities.FindAsync(userId);
            if (facility == null) return NotFound();

            facility.Name        = model.Name;
            facility.Address     = model.Address;
            facility.Description = model.Description;
            facility.Price       = model.Price;
            facility.PhotoUrl    = model.PhotoUrl;

            await _db.SaveChangesAsync();
            return Ok(facility);
        }

        [HttpGet("schedule/excel")]
        public async Task<IActionResult> ExportScheduleToExcel()
        {
            var schedules = await _db.Schedules
                .Include(s => s.Coach)
                .Include(s => s.SportFacility)
                .ToListAsync();

            using var pkg = new ExcelPackage();
            var ws = pkg.Workbook.Worksheets.Add("Schedule");
            ws.Cells[1, 1].Value = "Coach";
            ws.Cells[1, 2].Value = "Facility";
            ws.Cells[1, 3].Value = "Date";
            ws.Cells[1, 4].Value = "Start";
            ws.Cells[1, 5].Value = "End";

            int r = 2;
            foreach (var s in schedules)
            {
                ws.Cells[r, 1].Value = $"{s.Coach?.FirstName} {s.Coach?.LastName}";
                ws.Cells[r, 2].Value = s.SportFacility?.Name;
                ws.Cells[r, 3].Value = s.Date.ToString("yyyy-MM-dd");
                ws.Cells[r, 4].Value = s.StartTime.ToString(@"hh\:mm");
                ws.Cells[r, 5].Value = s.EndTime.ToString(@"hh\:mm");
                r++;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            var bytes = pkg.GetAsByteArray();
            return File(bytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "schedule.xlsx");
        }
    }
}
