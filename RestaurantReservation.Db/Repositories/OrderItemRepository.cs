using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderItemAsync(OrderItem newOrderItem)
        {
            var existingMenuItem = await _context.MenuItems.FindAsync(newOrderItem.ItemId);
            if (existingMenuItem == null)
            {
                throw new InvalidOperationException("The specified menu item does not exist.");
            }

            var existingOrder = await _context.Orders.FindAsync(newOrderItem.OrderId);
            if (existingOrder == null)
            {
                throw new InvalidOperationException("The specified order does not exist.");
            }

            _context.OrderItems.Add(newOrderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(int orderItemId, int newItemId, int newQuantity, int newOrderId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                var existingMenuItem = await _context.MenuItems.FindAsync(newItemId);
                if (existingMenuItem == null)
                {
                    throw new InvalidOperationException("The specified menu item does not exist.");
                }

                var existingOrder = await _context.Orders.FindAsync(newOrderId);
                if (existingOrder == null)
                {
                    throw new InvalidOperationException("The specified order does not exist.");
                }

                orderItem.ItemId = newItemId;
                orderItem.Quantity = newQuantity;
                orderItem.OrderId = newOrderId;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Order Item");
            }
            else
            {
                throw new KeyNotFoundException("Order Item not found.");
            }
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                Console.WriteLine("Deleted Order Item");
            }
            else
            {
                throw new KeyNotFoundException("Order Item not found.");
            }
        }
    }
}
