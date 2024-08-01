using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class DocumentRepository
    {
        private readonly AppDBContext _context;

        public DocumentRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddDocumentAsync(OrderDocument document)
        {
            _context.OrderDocuments.Add(document);
            await _context.SaveChangesAsync();
        }
    }
}
