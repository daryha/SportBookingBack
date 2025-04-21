// Controllers/PublicFacilitiesController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/facilities")]
    [ApiController]
    public class PublicFacilitiesController : ControllerBase
    {
        private readonly ISportFacilityService _svc;
        public PublicFacilitiesController(ISportFacilityService svc) => _svc = svc;

        // === ВСЕ ЭНДПОИНТЫ ===

        // 1) По городу
        // GET /api/facilities/city/Almaty
        [HttpGet("city/{city}"), AllowAnonymous]
        public async Task<IActionResult> GetByCity(string city)
        {
            var list = await _svc.GetFilteredFacilitiesAsync(
                city, null, null, null);
            return Ok(list);
        }

        // 2) По цене
        // GET /api/facilities/price?min=30&max=80
        [HttpGet("price"), AllowAnonymous]
        public async Task<IActionResult> GetByPrice(
            [FromQuery(Name = "min")] decimal minPrice,
            [FromQuery(Name = "max")] decimal maxPrice)
        {
            var list = await _svc.GetFilteredFacilitiesAsync(
                null, minPrice, maxPrice, null);
            return Ok(list);
        }

        // 3) По типу спорта
        // GET /api/facilities/type/Футбольный
        [HttpGet("type/{sportType}"), AllowAnonymous]
        public async Task<IActionResult> GetByType(string sportType)
        {
            var list = await _svc.GetFilteredFacilitiesAsync(
                null, null, null, sportType);
            return Ok(list);
        }

        // Оставляем старый комбинированный, если он нужен
        // GET /api/facilities?city=&minPrice=&maxPrice=&sportType=
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? city,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? sportType)
        {
            var list = await _svc.GetFilteredFacilitiesAsync(
                city, minPrice, maxPrice, sportType);
            return Ok(list);
        }

        // По ID
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var f = await _svc.GetFacilityByIdAsync(id);
            if (f == null) return NotFound();
            return Ok(f);
        }
    }
}
