// Controllers/PublicCoachesController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/coaches")]
    [ApiController]
    public class PublicCoachesController : ControllerBase
    {
        private readonly ICoachService _svc;
        public PublicCoachesController(ICoachService svc) => _svc = svc;

        // GET /api/coaches
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllCoachesAsync());

        // GET /api/coaches/{id}
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var coach = await _svc.GetCoachByIdAsync(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        // GET /api/coaches/price?min=..&max=..
        [HttpGet("price"), AllowAnonymous]
        public async Task<IActionResult> GetByPrice(
            [FromQuery(Name = "min")] decimal? min,
            [FromQuery(Name = "max")] decimal? max)
        {
            var list = await _svc.GetFilteredCoachesAsync(min, max, null);
            return Ok(list);
        }

        // GET /api/coaches/type/{sportType}
        [HttpGet("type/{sportType}"), AllowAnonymous]
        public async Task<IActionResult> GetByType(string sportType)
        {
            var list = await _svc.GetFilteredCoachesAsync(null, null, sportType);
            return Ok(list);
        }

        // GET /api/coaches/filter?minPrice=&maxPrice=&sportType=
        [HttpGet("filter"), AllowAnonymous]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? sportType)
        {
            var list = await _svc.GetFilteredCoachesAsync(minPrice, maxPrice, sportType);
            return Ok(list);
        }
    }
}
