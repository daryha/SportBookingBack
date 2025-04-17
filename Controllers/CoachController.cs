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

        // Логика для обновления информации о тренере
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCoachInfo([FromBody] Coach model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            // Обновление информации о тренере
            return Ok(new { message = "Информация обновлена" });
        }

        // Логика для выгрузки расписания тренера в Excel
        [HttpGet("{coachId}/schedule/excel")]
        public async Task<IActionResult> ExportCoachScheduleToExcel(string coachId)
        {
            // Получаем данные расписания для конкретного тренера
            var coach = await _context.Coaches
                .Include(c => c.Schedules)
                    .ThenInclude(s => s.SportFacility) // Включаем связанные спортивные площадки
                .FirstOrDefaultAsync(c => c.Id == coachId);

            if (coach == null)
                return NotFound("Тренер не найден.");

            // Создаем новый Excel пакет
            using (var package = new ExcelPackage())
            {
                // Добавляем рабочий лист
                var worksheet = package.Workbook.Worksheets.Add("Coach Schedule");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "Coach Name";
                worksheet.Cells[1, 2].Value = "Sport Facility";
                worksheet.Cells[1, 3].Value = "Date";
                worksheet.Cells[1, 4].Value = "Start Time";
                worksheet.Cells[1, 5].Value = "End Time";

                // Заполнение данных
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

                // Устанавливаем авторазмер для всех столбцов
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Генерируем файл в формате Excel
                var fileContent = package.GetAsByteArray();

                // Отправляем файл в ответе
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{coach.FirstName}_{coach.LastName}_schedule.xlsx");
            }
        }
    }
}
