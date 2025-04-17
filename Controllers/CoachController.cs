using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using System.Security.Claims;
using BookingSports.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BookingSports.Controllers
{
    [Authorize(Roles = "Coach")]
    [Route("api/coach")]
    public class CoachController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CoachController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ������ ��� ���������� ���������� � �������
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCoachInfo([FromBody] Coach model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            // ���������� ���������� � �������
            return Ok(new { message = "���������� ���������" });
        }

        // ������ ��� �������� ���������� ������� � Excel
        [HttpGet("{coachId}/schedule/excel")]
        public async Task<IActionResult> ExportCoachScheduleToExcel(string coachId)
        {
            // �������� ������ ���������� ��� ����������� �������
            var coach = await _context.Coaches
                .Include(c => c.Schedules)
                    .ThenInclude(s => s.SportFacility) // �������� ��������� ���������� ��������
                .FirstOrDefaultAsync(c => c.Id == coachId);

            if (coach == null)
                return NotFound("������ �� ������.");

            // ������� ����� Excel �����
            using (var package = new ExcelPackage())
            {
                // ��������� ������� ����
                var worksheet = package.Workbook.Worksheets.Add("Coach Schedule");

                // ��������� ��������
                worksheet.Cells[1, 1].Value = "Coach Name";
                worksheet.Cells[1, 2].Value = "Sport Facility";
                worksheet.Cells[1, 3].Value = "Date";
                worksheet.Cells[1, 4].Value = "Start Time";
                worksheet.Cells[1, 5].Value = "End Time";

                // ���������� ������
                int row = 2;
                foreach (var schedule in coach.Schedules)
                {
                    worksheet.Cells[row, 1].Value = coach.FirstName + " " + coach.LastName;
                    worksheet.Cells[row, 2].Value = schedule.SportFacility?.Name;
                    worksheet.Cells[row, 3].Value = schedule.Date.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 4].Value = schedule.StartTime.ToString(@"hh\:mm");
                    worksheet.Cells[row, 5].Value = schedule.EndTime.ToString(@"hh\:mm");
                    row++;
                }

                // ������������� ���������� ��� ���� ��������
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // ���������� ���� � ������� Excel
                var fileContent = package.GetAsByteArray();

                // ���������� ���� � ������
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{coach.FirstName}_{coach.LastName}_schedule.xlsx");
            }
        }
    }
}
