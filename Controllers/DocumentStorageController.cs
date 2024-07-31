using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentStorageController : Controller
    {
        private readonly AppDBContext _context;

        public DocumentStorageController(AppDBContext context) { _context = context; }

        [HttpPost("Save")]
        public async Task<ActionResult<bool>> PostOrder([FromForm] int DocumentID, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileData = memoryStream.ToArray();

                        OrderDocument document = new OrderDocument(DocumentID, file.FileName, fileData,DateTime.Now);

                        _context.OrderDocuments.Add(document);
                        await _context.SaveChangesAsync();
                    }
                }

                return Ok(true);
            }

            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return BadRequest($"An error occurred while saving the document: {ex.InnerException?.Message}");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while saving the document: {ex.Message}");
            }
        }


    }
}
