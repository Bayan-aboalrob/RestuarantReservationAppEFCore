using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContexts;
using RestaurantReservation.Db.Models;

public class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;

    public TableRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Table>> GetAllTablesAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    public async Task<Table> GetTableByIdAsync(int id)
    {
        return await _context.Tables.FindAsync(id);
    }

    public async Task AddTableAsync(Table table)
    {
        await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTableAsync(Table table)
    {
        _context.Tables.Update(table);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTableAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);
        if (table != null)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}

public interface ITableRepository
{
    Task<IEnumerable<Table>> GetAllTablesAsync();
    Task<Table> GetTableByIdAsync(int id);
    Task AddTableAsync(Table table);
    Task UpdateTableAsync(Table table);
    Task DeleteTableAsync(int id);
}
