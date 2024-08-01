using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class CustomerRepository
    {
        private readonly AppDBContext context;

        public CustomerRepository(AppDBContext context) { this.context = context; }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers.ToListAsync();
        }

    }
}
