using BeerTap.Models;
using BeerTap.Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerTap.Data.Repositories.OrderItem
{
    public class OrderItemRepository : Repository<BeerTap.Models.OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(BarDbContext context) : base(context)
        {
        }
    }
}
