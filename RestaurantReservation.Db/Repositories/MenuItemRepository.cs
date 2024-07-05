using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class MenuItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public MenuItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddMenuItemAsync(MenuItem newMenuItem)
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(newMenuItem.RestaurantId);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("The specified restaurant does not exist.");
            }

            var existingMenuItem = await _context.MenuItems
                .Where(m => m.Name == newMenuItem.Name && m.RestaurantId == newMenuItem.RestaurantId)
                .FirstOrDefaultAsync();

            if (existingMenuItem != null)
            {
                throw new InvalidOperationException("A menu item with the same name already exists in this restaurant.");
            }

            _context.MenuItems.Add(newMenuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(int menuItemId, string newName, string newDescription, decimal newPrice, int newRestaurantId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem != null)
            {
                var existingRestaurant = await _context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    throw new InvalidOperationException("The specified restaurant does not exist.");
                }

                var existingMenuItem = await _context.MenuItems
                    .FirstOrDefaultAsync(m => m.Name == newName && m.RestaurantId == newRestaurantId && m.ItemId != menuItemId);

                if (existingMenuItem != null)
                {
                    throw new InvalidOperationException("A menu item with the same name already exists in this restaurant.");
                }

                menuItem.Name = newName;
                menuItem.Description = newDescription;
                menuItem.Price = newPrice;
                menuItem.RestaurantId = newRestaurantId;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Menu Item");
            }
            else
            {
                throw new KeyNotFoundException("Menu Item not found.");
            }
        }

        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems
                .Include(m => m.OrderItems)
                .FirstOrDefaultAsync(m => m.ItemId == menuItemId);

            if (menuItem != null)
            {
                _context.OrderItems.RemoveRange(menuItem.OrderItems);
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Menu Item not found.");
            }
        }
    }
}
