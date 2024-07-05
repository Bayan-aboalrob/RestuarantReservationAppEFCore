using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeAsync(Employee newEmployee)
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(newEmployee.RestaurantId);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("The specified restaurant does not exist.");
            }

            var existingEmployee = await _context.Employees
                .Where(e => e.FirstName == newEmployee.FirstName && e.LastName == newEmployee.LastName && e.RestaurantId == newEmployee.RestaurantId)
                .FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                throw new InvalidOperationException("An employee with the same name already exists in this restaurant.");
            }

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(int employeeId, string newFirstName, string newLastName, string newPosition, int newRestaurantId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                var existingRestaurant = await _context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    throw new InvalidOperationException("The specified restaurant does not exist.");
                }

                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.FirstName == newFirstName && e.LastName == newLastName && e.RestaurantId == newRestaurantId && e.EmployeeId != employeeId);

                if (existingEmployee != null)
                {
                    throw new InvalidOperationException("An employee with the same name already exists in this restaurant.");
                }

                employee.FirstName = newFirstName;
                employee.LastName = newLastName;
                employee.Position = newPosition;
                employee.RestaurantId = newRestaurantId;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Employee");
            }
            else
            {
                throw new KeyNotFoundException("Employee not found.");
            }
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee != null)
            {
                foreach (var order in employee.Orders)
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                    _context.Orders.Remove(order);
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Employee not found.");
            }
        }

        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _context.Employees.Where(e => e.Position == "Manager").ToListAsync();
        }

        public async Task<List<EmployeeWithRestaurantDetails>> ListEmployeesWithRestaurantDetailsAsync()
        {
            return await _context.EmployeesWithRestaurantDetails.ToListAsync();
        }
    }
}
