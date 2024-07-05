using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            var existingEmployee = await _context.Employees.FindAsync(newOrder.EmployeeId);
            if (existingEmployee == null)
            {
                throw new InvalidOperationException("The specified employee does not exist.");
            }

            var existingReservation = await _context.Reservations.FindAsync(newOrder.ReservationId);
            if (existingReservation == null)
            {
                throw new InvalidOperationException("The specified reservation does not exist.");
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int orderId, DateTime newOrderDate, decimal newTotalAmount, int newEmployeeId, int newReservationId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                var existingEmployee = await _context.Employees.FindAsync(newEmployeeId);
                if (existingEmployee == null)
                {
                    throw new InvalidOperationException("The specified employee does not exist.");
                }

                var existingReservation = await _context.Reservations.FindAsync(newReservationId);
                if (existingReservation == null)
                {
                    throw new InvalidOperationException("The specified reservation does not exist.");
                }

                order.OrderDate = newOrderDate;
                order.TotalAmount = newTotalAmount;
                order.EmployeeId = newEmployeeId;
                order.ReservationId = newReservationId;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Order");
            }
            else
            {
                throw new KeyNotFoundException("Order not found.");
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Order not found.");
            }
        }

        public async Task<List<Order>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            var orders = await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            if (orders.Count == 0)
            {
                throw new KeyNotFoundException("No orders found for the given reservation ID.");
            }

            return orders;
        }

        public async Task<List<OrderItem>> ListOrderedMenuItemsAsync(int reservationId)
        {
            var orderedMenuItems = await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .SelectMany(o => o.OrderItems)
                .Include(oi => oi.MenuItem)
                .ToListAsync();

            if (orderedMenuItems.Count == 0)
            {
                throw new KeyNotFoundException("No menu items found for the given reservation ID.");
            }

            return orderedMenuItems;
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var orders = await _context.Orders.Where(o => o.EmployeeId == employeeId).ToListAsync();

            if (orders.Count == 0)
            {
                throw new KeyNotFoundException("No orders found for the given employee ID.");
            }

            var averageOrderAmount = orders.Average(o => o.TotalAmount);
            return averageOrderAmount;
        }
    }
}
