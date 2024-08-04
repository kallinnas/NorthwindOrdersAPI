using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TempOrderController : ControllerBase
    {
        private readonly TempOrderRepository tempOrderRepository;

        public TempOrderController(TempOrderRepository tempOrderRepository)
        {
            this.tempOrderRepository = tempOrderRepository;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveTempOrder([FromBody] TempOrder tempOrder)
        {
            var id = await tempOrderRepository.SaveTempOrderAsync(tempOrder);
            return Ok(new { id });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateTempOrder(Guid id, [FromBody] TempOrder tempOrder)
        {
            return Ok(await tempOrderRepository.UpdateTempOrderAsync(id, tempOrder));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetTempOrder(Guid id)
        {
            var tempOrder = await tempOrderRepository.GetTempOrderAsync(id);
            if (tempOrder == null) return NotFound();

            return Ok(tempOrder);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTempOrder(Guid id)
        {
            var result = await tempOrderRepository.DeleteTempOrderAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }

}
