using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _inventoryService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _inventoryService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound("Item not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Inventory item)
        {
            var createdItem = await _inventoryService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Inventory item)
        {
            var updatedItem = await _inventoryService.UpdateItemAsync(id, item);
            if (updatedItem == null)
                return NotFound("Item not found.");
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var isDeleted = await _inventoryService.DeleteItemAsync(id);
            if (!isDeleted)
                return NotFound("Item not found.");
            return NoContent();
        }
    }
}

