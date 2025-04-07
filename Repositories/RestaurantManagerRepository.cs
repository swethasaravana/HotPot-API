using HotPotAPI.Contexts;
using HotPotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotPotAPI.Repositories
{
    public class RestaurantManagerRepository : Repository<int, RestaurantManager>
    {
        public RestaurantManagerRepository(HotPotDbContext context) : base(context)
        {
        }

        public override async Task<RestaurantManager> GetById(int id)
        {
            var manager = await _context.RestaurantManagers
                .SingleOrDefaultAsync(rm => rm.Id == id);

            if (manager == null)
                throw new Exception($"Restaurant Manager with ID {id} not found");

            return manager;
        }

        public override async Task<IEnumerable<RestaurantManager>> GetAll()
        {
            var managers = await _context.RestaurantManagers
                .Include(rm => rm.Restaurant) 
                .ToListAsync();

            if (managers.Count == 0)
                throw new Exception("No restaurant managers found");

            return managers;
        }
    }
}


