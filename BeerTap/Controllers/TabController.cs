using BeerTap.Models;
using BeerTap.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabController : ControllerBase
    {
        private readonly TabService _tabService;

        public TabController(TabService tabService)
        {
            _tabService = tabService;
        }

        [HttpGet("beverages")]
        public async Task<ActionResult<IEnumerable<Beverage>>> GetBeverages()
        {
            var beverages = await _tabService.GetBeveragesAsync();
            return Ok(beverages);
        }

        [HttpGet("tabs")]
        public async Task<ActionResult<IEnumerable<Tab>>> GetAllTabs()
        {
            var tabs = await _tabService.GetAllTabsAsync();
            return Ok(tabs);
        }

        [HttpGet("tabs/{id}")]
        public async Task<ActionResult<Tab>> GetTabById(Guid id)
        {
            var tab = await _tabService.GetTabByIdAsync(id);
            if (tab == null) return NotFound("Tab not found.");

            return Ok(tab);
        }

        [HttpPost("order/{numberOfPeople}")]
        public async Task<ActionResult<Tab>> AddOrder(int numberOfPeople, [FromBody] List<OrderItem> orderItems)
        {
            var tab = await _tabService.CreateTabAsync(orderItems, numberOfPeople);
            return CreatedAtAction(nameof(GetTabById), new { id = tab.Id }, tab);
        }

        [HttpPost("split/{tabId}")]
        public async Task<ActionResult<Tab>> SplitBill(Guid tabId, [FromBody] int numberOfPeople)
        {
            var tab = await _tabService.SplitBillAsync(tabId, numberOfPeople);
            if (tab == null) return NotFound("Tab not found.");

            return Ok(tab);
        }

        [HttpGet("export")]
        public async Task<ActionResult> ExportTab()
        {
            var tabs = await _tabService.GetAllTabsAsync();
            var tab = tabs.LastOrDefault();
            if (tab == null) return NotFound("No active tab found.");

            // Export logic for CSV or PDF
            // For simplicity, let's return the tab as JSON
            return Ok(tab);
        }
    }
}
