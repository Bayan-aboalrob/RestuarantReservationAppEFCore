using System;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public TableRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddTableAsync(Table newTable)
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(newTable.RestaurantId);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("The specified restaurant does not exist.");
            }

            _context.Tables.Add(newTable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(int tableId, int newCapacity, int newRestaurantId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                var existingRestaurant = await _context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    throw new InvalidOperationException("The specified restaurant does not exist.");
                }

                table.Capacity = newCapacity;
                table.RestaurantId = newRestaurantId;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Table not found.");
            }
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables
                .Include(t => t.Reservations)
                .ThenInclude(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(t => t.TableId == tableId);

            if (table != null)
            {
                foreach (var reservation in table.Reservations)
                {
                    foreach (var order in reservation.Orders)
                    {
                        _context.OrderItems.RemoveRange(order.OrderItems);
                        _context.Orders.Remove(order);
                    }
                    _context.Reservations.Remove(reservation);
                }

                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Table not found.");
            }
        }

        public async Task<List<Table>> GetTablesByRestaurantAsync(int restaurantId)
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("The specified restaurant does not exist.");
            }

            return await _context.Tables
                .Where(t => t.RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}
