using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<Employee>CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllManagersAsync();
        Task<double> GetAverageOrderAmountByEmployeeIdAsync(int employeeId);
    }
}
