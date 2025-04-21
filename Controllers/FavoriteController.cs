// Controllers/FavoriteController.cs
using BookingSports.Models;
using BookingSports.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _svc;
        public FavoriteController(IFavoriteService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites() =>
            Ok(await _svc.GetAllFavoritesAsync());

        [HttpPost]
        public async Task<ActionResult<Favorite>> AddToFavorites([FromBody] Favorite fav)
        {
            var created = await _svc.AddToFavoritesAsync(fav);
            return CreatedAtAction(nameof(GetFavorites), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromFavorites(string id) =>
            await _svc.RemoveFromFavoritesAsync(id) ? NoContent() : NotFound();
    }
}
