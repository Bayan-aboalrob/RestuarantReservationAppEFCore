using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer newCustomer)
        {
            var existingCustomer = await _context.Customers
                .Where(c => c.Email == newCustomer.Email || c.PhoneNumber == newCustomer.PhoneNumber)
                .FirstOrDefaultAsync();

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("A customer with the same email or phone number already exists.");
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int customerId, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                var existingCustomerWithEmail = await _context.Customers.FirstOrDefaultAsync(c => c.Email == newEmail && c.CustomerId != customerId);
                var existingCustomerWithPhone = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == newPhoneNumber && c.CustomerId != customerId);

                if (existingCustomerWithEmail != null)
                {
                    throw new InvalidOperationException("Email already exists for another customer.");
                }

                if (existingCustomerWithPhone != null)
                {
                    throw new InvalidOperationException("Phone number already exists for another customer.");
                }

                customer.FirstName = newFirstName;
                customer.LastName = newLastName;
                customer.Email = newEmail;
                customer.PhoneNumber = newPhoneNumber;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Customer not found.");
            }
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Reservations)
                .ThenInclude(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer != null)
            {
                foreach (var reservation in customer.Reservations)
                {
                    foreach (var order in reservation.Orders)
                    {
                        _context.OrderItems.RemoveRange(order.OrderItems);
                        _context.Orders.Remove(order);
                    }
                    _context.Reservations.Remove(reservation);
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Customer not found.");
            }
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            return await _context.Reservations
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.Customer)
                .ToListAsync();
        }
        public async Task<List<Customer>> GetCustomersWithLargeReservationsAsync(int partySize)
        {
            return await _context.Customers
                .Where(c => c.Reservations.Any(r => r.PartySize >= partySize))
                .ToListAsync();
        }
    }
}
