using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Services;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentStorageController : Controller
    {
        private readonly DocumentService documentService;

        public DocumentStorageController(DocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost("Save")]
        public async Task<ActionResult<bool>> PostOrder([FromForm] int documentID, IFormFile file)
        {
            await documentService.SaveDocumentAsync(documentID, file);
            return Ok(true);
        }


    }
}
