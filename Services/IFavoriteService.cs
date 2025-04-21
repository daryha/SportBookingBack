// Services/IFavoriteService.cs
using BookingSports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync();
        Task<Favorite>              AddToFavoritesAsync(Favorite favorite);
        Task<bool>                  RemoveFromFavoritesAsync(string id);
    }
}
