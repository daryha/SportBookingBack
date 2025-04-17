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
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // Получить все избранные элементы пользователя
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            var favorites = await _favoriteService.GetAllFavoritesAsync();
            return Ok(favorites);
        }

        // Добавить объект в избранное
        [HttpPost]
        public async Task<ActionResult<Favorite>> AddToFavorites([FromBody] Favorite favorite)
        {
            var addedFavorite = await _favoriteService.AddToFavoritesAsync(favorite);
            return CreatedAtAction(nameof(GetFavorites), new { id = addedFavorite.Id }, addedFavorite);
        }

        // Удалить объект из избранного
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromFavorites(string id)
        {
            var success = await _favoriteService.RemoveFromFavoritesAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
