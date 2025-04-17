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
    [Authorize(Roles = "SportFacility")]
    [Route("api/facility")]
    public class FacilityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FacilityController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // Логика для обновления информации о спортивной площадке
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFacilityInfo([FromBody] SportFacility model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            // Обновление информации о спортивной площадке
            return Ok(new { message = "Информация о площадке обновлена" });
        }

        // Логика для выгрузки данных в Excel
        [HttpGet("schedule/excel")]
        public async Task<IActionResult> ExportScheduleToExcel()
        {
            // Получаем данные расписания из базы данных
            var schedules = await _context.Schedules
                .Include(s => s.Coach)
                .Include(s => s.SportFacility)
                .ToListAsync();

            // Создаем новый Excel пакет
            using (var package = new ExcelPackage())
            {
                // Добавляем рабочий лист
                var worksheet = package.Workbook.Worksheets.Add("Schedule");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "Coach Name";
                worksheet.Cells[1, 2].Value = "Sport Facility Name";
                worksheet.Cells[1, 3].Value = "Date";
                worksheet.Cells[1, 4].Value = "Start Time";
                worksheet.Cells[1, 5].Value = "End Time";

                // Заполнение данных
                int row = 2;
                foreach (var schedule in schedules)
                {
                    worksheet.Cells[row, 1].Value = schedule.Coach?.FirstName + " " + schedule.Coach?.LastName;
                    worksheet.Cells[row, 2].Value = schedule.SportFacility?.Name;
                    worksheet.Cells[row, 3].Value = schedule.Date.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 4].Value = schedule.StartTime.ToString(@"hh\:mm");
                    worksheet.Cells[row, 5].Value = schedule.EndTime.ToString(@"hh\:mm");
                    row++;
                }

                // Устанавливаем авторазмер для всех столбцов
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Генерируем файл в формате Excel
                var fileContent = package.GetAsByteArray();

                // Отправляем файл в ответе
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "schedule.xlsx");
            }
        }
    }
}
