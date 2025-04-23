using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HotelBookingApi.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await _inventoryService.GetAllItems(cancellationToken);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await _inventoryService.GetItemById(id, cancellationToken);
            if (item == null)
                return NotFound("Item not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inventory item, CancellationToken cancellationToken)
        {
            var createdItem = await _inventoryService.CreateItem(item, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Inventory item, CancellationToken cancellationToken)
        {
            var updatedItem = await _inventoryService.UpdateItem(id, item, cancellationToken);
            if (updatedItem == null)
                return NotFound("Item not found.");
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _inventoryService.DeleteItem(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Item not found.");
            return NoContent();
        }
    }
}

