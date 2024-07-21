using BeerTap.Models;
using BeerTap.Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerTap.Data.Repositories.Beverage
{
    public class BeverageRepository : Repository<BeerTap.Models.Beverage>, IBeverageRepository
    {
        public BeverageRepository(BarDbContext context) : base(context)
        {
        }
    }
}
