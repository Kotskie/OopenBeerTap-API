using BeerTap.Models;
using BeerTap.Data.Repositories.Beverage;
using BeerTap.Data.Repositories.OrderItem;
using BeerTap.Data.Repositories.Tab;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerTap.Data;

namespace BeerTap.Services
{
    public class TabService
    {
        private readonly IBeverageRepository _beverageRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ITabRepository _tabRepository;
        private readonly BarDbContext _context;

        public TabService(IBeverageRepository beverageRepository, IOrderItemRepository orderItemRepository, ITabRepository tabRepository, BarDbContext context)
        {
            _beverageRepository = beverageRepository;
            _orderItemRepository = orderItemRepository;
            _tabRepository = tabRepository;
            _context = context;
        }

        public async Task<IEnumerable<Beverage>> GetBeveragesAsync()
        {
            return await _beverageRepository.GetAllAsync();
        }

        public async Task<Tab> CreateTabAsync(List<OrderItem> orderItems, int? numberOfPeople)
        {
            foreach (var item in orderItems)
            {
                item.Id = Guid.NewGuid();
                var existingItem = await _orderItemRepository.GetByIdAsync(item.Id);
                if (existingItem == null)
                {
                    await _orderItemRepository.AddAsync(item);
                }
            }

            var tab = new Tab
            {
                Id = Guid.NewGuid(),
                Items = orderItems,
                Total = orderItems.Sum(item => item.Beverage.Price * item.Quantity)
            };

            if (numberOfPeople > 0)
            {
                tab.SplitBetween = numberOfPeople;
                tab.TotalPerPerson = tab.Total / numberOfPeople;
            }

            await _tabRepository.AddAsync(tab);
            return tab;
        }

        public async Task<IEnumerable<Tab>> GetAllTabsAsync()
        {
            return await _tabRepository.GetAllAsync();
        }

        public async Task<Tab> GetTabByIdAsync(Guid id)
        {
            return await _tabRepository.GetByIdAsync(id);
        }

        public async Task<Tab> UpdateTabAsync(Tab tab)
        {
            await _tabRepository.UpdateAsync(tab);
            return tab;
        }

        public async Task<bool> DeleteTabAsync(Guid id)
        {
            var tab = await _tabRepository.GetByIdAsync(id);
            if (tab == null)
            {
                return false;
            }

            await _tabRepository.UpdateAsync(tab);
            return true;
        }

        public async Task<Tab> SplitBillAsync(Guid tabId, int numberOfPeople)
        {
            var tab = await _tabRepository.GetByIdAsync(tabId);
            if (tab != null)
            {
                tab.SplitBetween = numberOfPeople;
                tab.TotalPerPerson = tab.Total / numberOfPeople;
                await _tabRepository.UpdateAsync(tab);
            }
            return tab;
        }
    }
}
