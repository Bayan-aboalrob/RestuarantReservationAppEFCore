using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public RestaurantRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddRestaurantAsync(Restaurant newRestaurant)
        {
            var existingRestaurant = await _context.Restaurants
                .Where(r => r.Name == newRestaurant.Name)
                .FirstOrDefaultAsync();

            if (existingRestaurant != null)
            {
                throw new InvalidOperationException("A restaurant with the same name already exists.");
            }

            _context.Restaurants.Add(newRestaurant);
            await _context.SaveChangesAsync();
            Console.WriteLine("Created Restaurant");
        }

        public async Task UpdateRestaurantAsync(int restaurantId, string newName, string newAddress, string newPhoneNumber, string newOpeningHours)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                var existingRestaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Name == newName && r.RestaurantId != restaurantId);

                if (existingRestaurant != null)
                {
                    throw new InvalidOperationException("A restaurant with the same name already exists.");
                }

                restaurant.Name = newName;
                restaurant.Address = newAddress;
                restaurant.PhoneNumber = newPhoneNumber;
                restaurant.OpeningHours = newOpeningHours;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Restaurant");
            }
            else
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Employees)
                .ThenInclude(e => e.Orders)
                .ThenInclude(o => o.OrderItems)
                .Include(r => r.MenuItems)
                .ThenInclude(m => m.OrderItems)
                .Include(r => r.Reservations)
                .ThenInclude(res => res.Orders)
                .ThenInclude(o => o.OrderItems)
                .Include(r => r.Tables)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (restaurant != null)
            {
                foreach (var employee in restaurant.Employees)
                {
                    foreach (var order in employee.Orders)
                    {
                        _context.OrderItems.RemoveRange(order.OrderItems);
                        _context.Orders.Remove(order);
                    }
                    _context.Employees.Remove(employee);
                }

                foreach (var reservation in restaurant.Reservations)
                {
                    foreach (var order in reservation.Orders)
                    {
                        _context.OrderItems.RemoveRange(order.OrderItems);
                        _context.Orders.Remove(order);
                    }
                    _context.Reservations.Remove(reservation);
                }

                foreach (var menuItem in restaurant.MenuItems)
                {
                    _context.OrderItems.RemoveRange(menuItem.OrderItems);
                    _context.MenuItems.Remove(menuItem);
                }

                _context.Tables.RemoveRange(restaurant.Tables);
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
                Console.WriteLine("Deleted Restaurant");
            }
            else
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }
        }

        public async Task<decimal> CalculateTotalRevenueAsync(int restaurantId)
        {
            var revenue = await _context.Set<TotalRevenue>()
                .FromSqlRaw("SELECT dbo.CalculateTotalRevenue({0}) AS Value", restaurantId)
                .FirstOrDefaultAsync();

            return revenue?.Value ?? 0m;
        }
    }
}
