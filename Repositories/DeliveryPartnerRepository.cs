using HotPotAPI.Contexts;
using HotPotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotPotAPI.Repositories
{
    public class DeliveryPartnerRepository : Repository<int, DeliveryPartner>
    {
        public DeliveryPartnerRepository(HotPotDbContext context) : base(context)
        {
        }

        public override async Task<DeliveryPartner> GetById(int id)
        {
            var partner = await _context.DeliveryPartners
                .Include(dp => dp.User)
                .SingleOrDefaultAsync(dp => dp.PartnerId == id); 

            if (partner == null)
                throw new Exception($"Delivery Partner with ID {id} not found");

            return partner;
        }

        public override async Task<IEnumerable<DeliveryPartner>> GetAll()
        {
            var partners = await _context.DeliveryPartners
                .Include(dp => dp.User)
                .ToListAsync();

            if (partners.Count == 0)
                throw new Exception("No delivery partners found");

            return partners;
        }
    }
}
