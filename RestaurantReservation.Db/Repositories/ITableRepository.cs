using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int id);
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(int id);
    }
}
