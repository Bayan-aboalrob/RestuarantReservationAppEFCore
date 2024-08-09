using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContexts;
using RestaurantReservation.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Customer not found.");
            }
        }
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
