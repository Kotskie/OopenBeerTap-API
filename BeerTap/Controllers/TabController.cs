using BeerTap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabController : ControllerBase
    {
        private static readonly List<Beverage> Beverages = new List<Beverage>
    {
        new Beverage { Id = Guid.NewGuid(), Name = "Beer", Price = 45.00m },
        new Beverage { Id = Guid.NewGuid(), Name = "Cider", Price = 52.00m },
        new Beverage { Id = Guid.NewGuid(), Name = "Premix", Price = 59.00m }
    };

        private static readonly List<Tab> Tabs = new List<Tab>();

        [HttpGet("beverages")]
        public ActionResult<IEnumerable<Beverage>> GetBeverages()
        {
            return Beverages;
        }

        [HttpPost("order")]
        public ActionResult<Tab> AddOrder([FromBody] List<OrderItem> orderItems)
        {
            var tab = new Tab
            {
                Items = orderItems,
                Total = orderItems.Sum(item => item.Beverage.Price * item.Quantity)
            };
            Tabs.Add(tab);
            return tab;
        }

        [HttpPost("split")]
        public ActionResult<Tab> SplitBill([FromBody] int numberOfPeople)
        {
            var tab = Tabs.LastOrDefault();
            if (tab == null) return NotFound("No active tab found.");

            tab.SplitBetween = numberOfPeople;
            tab.TotalPerPerson = tab.Total / numberOfPeople;
            return tab;
        }

        [HttpGet("export")]
        public ActionResult<Tab> ExportTab()
        {
            var tab = Tabs.LastOrDefault();
            if (tab == null) return NotFound("No active tab found.");

            return tab;
        }
    }
}
