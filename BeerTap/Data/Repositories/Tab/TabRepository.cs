using BeerTap.Models;
using BeerTap.Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerTap.Data.Repositories.Tab
{
    public class TabRepository : Repository<BeerTap.Models.Tab>, ITabRepository
    {
        public TabRepository(BarDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<BeerTap.Models.Tab>> GetAllAsync()
        {
            return await Context.Set<BeerTap.Models.Tab>()
                .Include(t => t.Items)
                .ThenInclude(i => i.Beverage)
                .ToListAsync();
        }

        public override async Task<BeerTap.Models.Tab> GetByIdAsync(Guid id)
        {
            return await Context.Set<BeerTap.Models.Tab>()
                .Include(t => t.Items)
                .ThenInclude(i => i.Beverage)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
