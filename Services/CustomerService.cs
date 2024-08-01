using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;

namespace NorthwindOrdersAPI.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await customerRepository.GetAllCustomersAsync();
        }
    }
}
