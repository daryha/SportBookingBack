// Services/FavoriteService.cs
using BookingSports.Data;
using BookingSports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSports.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _db;
        public FavoriteService(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
        {
            return await _db.Favorites
                .Include(f => f.User)
                .Include(f => f.Coach)
                .Include(f => f.SportFacility)
                .ToListAsync();
        }

        public async Task<Favorite> AddToFavoritesAsync(Favorite favorite)
        {
            favorite.Id = Guid.NewGuid().ToString();
            _db.Favorites.Add(favorite);
            await _db.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> RemoveFromFavoritesAsync(string id)
        {
            var f = await _db.Favorites.FindAsync(id);
            if (f == null) return false;
            _db.Favorites.Remove(f);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
