using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContexts;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Orders).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.Include(e => e.Orders)
                                            .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Employee>> GetAllManagersAsync()
        {
            return await _context.Employees
                .Where(e => e.Position == "Manager") // Adjust based on how positions are stored
                .ToListAsync();
        }

        public async Task<double> GetAverageOrderAmountByEmployeeIdAsync(int employeeId)
        {
            var orders = await _context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            if (!orders.Any())
                return 0;

            var totalAmount = orders.Sum(o => o.OrderItems
                .Sum(oi => (oi.MenuItem?.Price ?? 0) * oi.Quantity));

            var averageAmount = (double)totalAmount / orders.Count;

            return averageAmount;
        }


    }
}
