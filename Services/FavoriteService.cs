using BookingSports.Models;
using BookingSports.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync();
        Task<Favorite> AddToFavoritesAsync(Favorite favorite);
        Task<bool> RemoveFromFavoritesAsync(string id);
    }

    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить все избранные элементы пользователя
        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
        {
            return await _context.Favorites.ToListAsync();
        }

        // Добавить объект в избранное
        public async Task<Favorite> AddToFavoritesAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        // Удалить объект из избранного
        public async Task<bool> RemoveFromFavoritesAsync(string id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return false;
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
