using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;

namespace NorthwindOrdersAPI.Services
{
    public class DocumentService
    {
        private readonly DocumentRepository _documentRepository;

        public DocumentService(DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task SaveDocumentAsync(int documentID, IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    var document = new OrderDocument(documentID, file.FileName, fileData, DateTime.Now);
                    await _documentRepository.AddDocumentAsync(document);
                }
            }
        }
    }
}
