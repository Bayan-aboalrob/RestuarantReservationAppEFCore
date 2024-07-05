﻿using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Restaurant Reservation Application !");

            using (var context = new RestaurantReservationDbContext())
            {
                await context.Database.EnsureCreatedAsync();

                var newCustomer = new Customer
                {
                    FirstName = "Alex",
                    LastName = "Johnson",
                    Email = "alex.johnson@example.com", 
                    PhoneNumber = "123-456-7891"
                };
                await AddCustomerAsync(context, newCustomer);
                int customerId = newCustomer.CustomerId;

                var newEmployee = new Employee { FirstName = "Emily", LastName = "Brown", Position = "Waiter", RestaurantId = 3 };
                await AddEmployeeAsync(context, newEmployee);
                int employeeId = newEmployee.EmployeeId;

                var newMenuItem = new MenuItem { Name = "Salad", Description = "Greek Salad", Price = 7.99m, RestaurantId = 3 };
                await AddMenuItemAsync(context, newMenuItem);
                int menuItemId = newMenuItem.ItemId;

                var newReservation = new Reservation { RestaurantId = 3, ReservationDate = DateTime.Now.AddDays(1), PartySize = 4, CustomerId = customerId, TableId = 4 };
                await AddReservationAsync(context, newReservation);
                int reservationId = newReservation.ReservationId;

                var newOrder = new Order { EmployeeId = employeeId, OrderDate = DateTime.Now, TotalAmount = 29.99m, ReservationId = reservationId };
                await AddOrderAsync(context, newOrder);
                int orderId = newOrder.OrderId;

                var newOrderItem = new OrderItem { ItemId = menuItemId, Quantity = 2, OrderId = orderId };
                await AddOrderItemAsync(context, newOrderItem);
                int orderItemId = newOrderItem.OrderItemId;

                var newRestaurant = new Restaurant { Name = "New Sample Restaurant", Address = "Sample Address", PhoneNumber = "999-999-9988", OpeningHours = "8 AM - 8 PM" };
                await AddRestaurantAsync(context, newRestaurant);
                int restaurantId = newRestaurant.RestaurantId;

                var newTable = new Table { Capacity = 5, RestaurantId = restaurantId };
                await AddTableAsync(context, newTable);
                int tableId = newTable.TableId;

                await UpdateCustomerAsync(context, customerId, "Michael", "Smith", "michael.smith@example.com", "098-765-4321");

                await UpdateEmployeeAsync(context, employeeId, "Emily", "Jones", "Chef", 3);

                await UpdateMenuItemAsync(context, menuItemId, "Salad", "Caesar Salad", 8.99m, 3);

                await UpdateReservationAsync(context, reservationId, DateTime.Now.AddDays(2), 5, customerId, 3, 4);

                await UpdateOrderAsync(context, orderId, DateTime.Now.AddMinutes(30), 34.99m, employeeId, reservationId);

                await UpdateOrderItemAsync(context, orderItemId, menuItemId, 3, orderId);

                await UpdateRestaurantAsync(context, restaurantId, "Updated Sample Restaurant", "Updated Sample Address", "888-888-8877", "9 AM - 9 PM");

                await UpdateTableAsync(context, tableId, 6, restaurantId);

                await DeleteOrderItemAsync(context, orderItemId);

                await DeleteOrderAsync(context, orderId);

                await DeleteReservationAsync(context, reservationId);

                await DeleteCustomerAsync(context, customerId);

                await DeleteEmployeeAsync(context, employeeId);

                await DeleteMenuItemAsync(context, menuItemId);

                await DeleteRestaurantAsync(context, restaurantId);

                await DeleteTableAsync(context, tableId);

            }
        }

        // Here, I have added the Creates methods for creating new entities for each entity type
        static async Task AddCustomerAsync(RestaurantReservationDbContext context, Customer newCustomer)
        {
            var existingCustomer = await context.Customers
                .Where(c => c.Email == newCustomer.Email || c.PhoneNumber == newCustomer.PhoneNumber)
                .FirstOrDefaultAsync();

            if (existingCustomer != null)
            {
                Console.WriteLine("A customer with the same email or phone number already exists.");
                return;
            }

            context.Customers.Add(newCustomer);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Customer");
        }

        static async Task AddEmployeeAsync(RestaurantReservationDbContext context, Employee newEmployee)
        {
            // Check if the restaurant exists
            var existingRestaurant = await context.Restaurants.FindAsync(newEmployee.RestaurantId);
            if (existingRestaurant == null)
            {
                Console.WriteLine("The specified restaurant does not exist.");
                return;
            }

            var existingEmployee = await context.Employees
                .Where(e => e.FirstName == newEmployee.FirstName && e.LastName == newEmployee.LastName && e.RestaurantId == newEmployee.RestaurantId)
                .FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                Console.WriteLine("An employee with the same name already exists in this restaurant.");
                return;
            }

            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Employee");
        }

        static async Task AddMenuItemAsync(RestaurantReservationDbContext context, MenuItem newMenuItem)
        {
            var existingRestaurant = await context.Restaurants.FindAsync(newMenuItem.RestaurantId);
            if (existingRestaurant == null)
            {
                Console.WriteLine("The specified restaurant does not exist.");
                return;
            }

            var existingMenuItem = await context.MenuItems
                .Where(m => m.Name == newMenuItem.Name && m.RestaurantId == newMenuItem.RestaurantId)
                .FirstOrDefaultAsync();

            if (existingMenuItem != null)
            {
                Console.WriteLine("A menu item with the same name already exists in this restaurant.");
                return;
            }

            context.MenuItems.Add(newMenuItem);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Menu Item");
        }

        static async Task AddOrderAsync(RestaurantReservationDbContext context, Order newOrder)
        {
            var existingEmployee = await context.Employees.FindAsync(newOrder.EmployeeId);
            if (existingEmployee == null)
            {
                Console.WriteLine("The specified employee does not exist.");
                return;
            }

            var existingReservation = await context.Reservations.FindAsync(newOrder.ReservationId);
            if (existingReservation == null)
            {
                Console.WriteLine("The specified reservation does not exist.");
                return;
            }

            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Order");
        }

        static async Task AddOrderItemAsync(RestaurantReservationDbContext context, OrderItem newOrderItem)
        {
            var existingMenuItem = await context.MenuItems.FindAsync(newOrderItem.ItemId);
            if (existingMenuItem == null)
            {
                Console.WriteLine("The specified menu item does not exist.");
                return;
            }

            var existingOrder = await context.Orders.FindAsync(newOrderItem.OrderId);
            if (existingOrder == null)
            {
                Console.WriteLine("The specified order does not exist.");
                return;
            }

            context.OrderItems.Add(newOrderItem);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Order Item");
        }

        static async Task AddReservationAsync(RestaurantReservationDbContext context, Reservation newReservation)
        {
            var existingCustomer = await context.Customers.FindAsync(newReservation.CustomerId);
            if (existingCustomer == null)
            {
                Console.WriteLine("The specified customer does not exist.");
                return;
            }

            var existingRestaurant = await context.Restaurants.FindAsync(newReservation.RestaurantId);
            if (existingRestaurant == null)
            {
                Console.WriteLine("The specified restaurant does not exist.");
                return;
            }
            var existingTable = await context.Tables.FindAsync(newReservation.TableId);
            if (existingTable == null)
            {
                Console.WriteLine("The specified table does not exist.");
                return;
            }

            context.Reservations.Add(newReservation);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Reservation");
        }
        static async Task AddRestaurantAsync(RestaurantReservationDbContext context, Restaurant newRestaurant)
        {
            var existingRestaurant = await context.Restaurants
                .Where(r => r.Name == newRestaurant.Name)
                .FirstOrDefaultAsync();

            if (existingRestaurant != null)
            {
                Console.WriteLine("A restaurant with the same name already exists.");
                return;
            }

            context.Restaurants.Add(newRestaurant);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Restaurant");
        }

        static async Task AddTableAsync(RestaurantReservationDbContext context, Table newTable)
        {
            var existingRestaurant = await context.Restaurants.FindAsync(newTable.RestaurantId);
            if (existingRestaurant == null)
            {
                Console.WriteLine("The specified restaurant does not exist.");
                return;
            }

            context.Tables.Add(newTable);
            await context.SaveChangesAsync();
            Console.WriteLine("Created Table");
        }

        // Here, I have implemented the update methods for all entity types

        static async Task UpdateCustomerAsync(RestaurantReservationDbContext context, int customerId, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            var customer = await context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                var existingCustomerWithEmail = await context.Customers.FirstOrDefaultAsync(c => c.Email == newEmail && c.CustomerId != customerId);
                var existingCustomerWithPhone = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == newPhoneNumber && c.CustomerId != customerId);

                if (existingCustomerWithEmail != null)
                {
                    Console.WriteLine("Email already exists for another customer.");
                    return;
                }

                if (existingCustomerWithPhone != null)
                {
                    Console.WriteLine("Phone number already exists for another customer.");
                    return;
                }

                customer.FirstName = newFirstName;
                customer.LastName = newLastName;
                customer.Email = newEmail;
                customer.PhoneNumber = newPhoneNumber;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Customer");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static async Task UpdateEmployeeAsync(RestaurantReservationDbContext context, int employeeId, string newFirstName, string newLastName, string newPosition, int newRestaurantId)
        {
            var employee = await context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                var existingRestaurant = await context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    Console.WriteLine("The specified restaurant does not exist.");
                    return;
                }

                var existingEmployee = await context.Employees
                    .FirstOrDefaultAsync(e => e.FirstName == newFirstName && e.LastName == newLastName && e.RestaurantId == newRestaurantId && e.EmployeeId != employeeId);

                if (existingEmployee != null)
                {
                    Console.WriteLine("An employee with the same name already exists in this restaurant.");
                    return;
                }

                employee.FirstName = newFirstName;
                employee.LastName = newLastName;
                employee.Position = newPosition;
                employee.RestaurantId = newRestaurantId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Employee");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        static async Task UpdateMenuItemAsync(RestaurantReservationDbContext context, int menuItemId, string newName, string newDescription, decimal newPrice, int newRestaurantId)
        {
            var menuItem = await context.MenuItems.FindAsync(menuItemId);
            if (menuItem != null)
            {
                var existingRestaurant = await context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    Console.WriteLine("The specified restaurant does not exist.");
                    return;
                }

                var existingMenuItem = await context.MenuItems
                    .FirstOrDefaultAsync(m => m.Name == newName && m.RestaurantId == newRestaurantId && m.ItemId != menuItemId);

                if (existingMenuItem != null)
                {
                    Console.WriteLine("A menu item with the same name already exists in this restaurant.");
                    return;
                }

                menuItem.Name = newName;
                menuItem.Description = newDescription;
                menuItem.Price = newPrice;
                menuItem.RestaurantId = newRestaurantId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Menu Item");
            }
            else
            {
                Console.WriteLine("Menu Item not found.");
            }
        }
        static async Task UpdateOrderAsync(RestaurantReservationDbContext context, int orderId, DateTime newOrderDate, decimal newTotalAmount, int newEmployeeId, int newReservationId)
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order != null)
            {
                var existingEmployee = await context.Employees.FindAsync(newEmployeeId);
                if (existingEmployee == null)
                {
                    Console.WriteLine("The specified employee does not exist.");
                    return;
                }

                var existingReservation = await context.Reservations.FindAsync(newReservationId);
                if (existingReservation == null)
                {
                    Console.WriteLine("The specified reservation does not exist.");
                    return;
                }

                order.OrderDate = newOrderDate;
                order.TotalAmount = newTotalAmount;
                order.EmployeeId = newEmployeeId;
                order.ReservationId = newReservationId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Order");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
        static async Task UpdateOrderItemAsync(RestaurantReservationDbContext context, int orderItemId, int newItemId, int newQuantity, int newOrderId)
        {
            var orderItem = await context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                var existingMenuItem = await context.MenuItems.FindAsync(newItemId);
                if (existingMenuItem == null)
                {
                    Console.WriteLine("The specified menu item does not exist.");
                    return;
                }

                var existingOrder = await context.Orders.FindAsync(newOrderId);
                if (existingOrder == null)
                {
                    Console.WriteLine("The specified order does not exist.");
                    return;
                }

                orderItem.ItemId = newItemId;
                orderItem.Quantity = newQuantity;
                orderItem.OrderId = newOrderId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Order Item");
            }
            else
            {
                Console.WriteLine("Order Item not found.");
            }
        }
        static async Task UpdateReservationAsync(RestaurantReservationDbContext context, int reservationId, DateTime newReservationDate, int newPartySize, int newCustomerId, int newRestaurantId, int newTableId)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                var existingCustomer = await context.Customers.FindAsync(newCustomerId);
                if (existingCustomer == null)
                {
                    Console.WriteLine("The specified customer does not exist.");
                    return;
                }

                var existingRestaurant = await context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    Console.WriteLine("The specified restaurant does not exist.");
                    return;
                }

                var existingTable = await context.Tables.FindAsync(newTableId);
                if (existingTable == null)
                {
                    Console.WriteLine("The specified table does not exist.");
                    return;
                }

                reservation.ReservationDate = newReservationDate;
                reservation.PartySize = newPartySize;
                reservation.CustomerId = newCustomerId;
                reservation.RestaurantId = newRestaurantId;
                reservation.TableId = newTableId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Reservation");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }
        static async Task UpdateRestaurantAsync(RestaurantReservationDbContext context, int restaurantId, string newName, string newAddress, string newPhoneNumber, string newOpeningHours)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                var existingRestaurant = await context.Restaurants
                    .FirstOrDefaultAsync(r => r.Name == newName && r.RestaurantId != restaurantId);

                if (existingRestaurant != null)
                {
                    Console.WriteLine("A restaurant with the same name already exists.");
                    return;
                }

                restaurant.Name = newName;
                restaurant.Address = newAddress;
                restaurant.PhoneNumber = newPhoneNumber;
                restaurant.OpeningHours = newOpeningHours;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Restaurant");
            }
            else
            {
                Console.WriteLine("Restaurant not found.");
            }
        }
        static async Task UpdateTableAsync(RestaurantReservationDbContext context, int tableId, int newCapacity, int newRestaurantId)
        {
            var table = await context.Tables.FindAsync(tableId);
            if (table != null)
            {
                var existingRestaurant = await context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    Console.WriteLine("The specified restaurant does not exist.");
                    return;
                }

                table.Capacity = newCapacity;
                table.RestaurantId = newRestaurantId;

                await context.SaveChangesAsync();
                Console.WriteLine("Updated Table");
            }
            else
            {
                Console.WriteLine("Table not found.");
            }
        }
        static async Task DeleteCustomerAsync(RestaurantReservationDbContext context, int customerId)
        {
            var customer = await context.Customers
                .Include(c => c.Reservations)
                .ThenInclude(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer != null)
            {
                foreach (var reservation in customer.Reservations)
                {
                    foreach (var order in reservation.Orders)
                    {
                        context.OrderItems.RemoveRange(order.OrderItems);
                        context.Orders.Remove(order);
                    }
                    context.Reservations.Remove(reservation);
                }

                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Customer");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        static async Task DeleteEmployeeAsync(RestaurantReservationDbContext context, int employeeId)
        {
            var employee = await context.Employees
                .Include(e => e.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee != null)
            {
                foreach (var order in employee.Orders)
                {
                    context.OrderItems.RemoveRange(order.OrderItems);
                    context.Orders.Remove(order);
                }

                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Employee");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static async Task DeleteMenuItemAsync(RestaurantReservationDbContext context, int menuItemId)
        {
            var menuItem = await context.MenuItems
                .Include(m => m.OrderItems)
                .FirstOrDefaultAsync(m => m.ItemId == menuItemId);

            if (menuItem != null)
            {
                context.OrderItems.RemoveRange(menuItem.OrderItems);
                context.MenuItems.Remove(menuItem);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Menu Item");
            }
            else
            {
                Console.WriteLine("Menu Item not found.");
            }
        }
        static async Task DeleteOrderAsync(RestaurantReservationDbContext context, int orderId)
        {
            var order = await context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                context.OrderItems.RemoveRange(order.OrderItems);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Order");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }


        static async Task DeleteOrderItemAsync(RestaurantReservationDbContext context, int orderItemId)
        {
            var orderItem = await context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                context.OrderItems.Remove(orderItem);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Order Item");
            }
            else
            {
                Console.WriteLine("Order Item not found.");
            }
        }


        static async Task DeleteReservationAsync(RestaurantReservationDbContext context, int reservationId)
        {
            var reservation = await context.Reservations
                .Include(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation != null)
            {
                foreach (var order in reservation.Orders)
                {
                    context.OrderItems.RemoveRange(order.OrderItems);
                    context.Orders.Remove(order);
                }

                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Reservation");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }

        static async Task DeleteRestaurantAsync(RestaurantReservationDbContext context, int restaurantId)
        {
            var restaurant = await context.Restaurants
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
                        context.OrderItems.RemoveRange(order.OrderItems);
                        context.Orders.Remove(order);
                    }
                    context.Employees.Remove(employee);
                }

                foreach (var reservation in restaurant.Reservations)
                {
                    foreach (var order in reservation.Orders)
                    {
                        context.OrderItems.RemoveRange(order.OrderItems);
                        context.Orders.Remove(order);
                    }
                    context.Reservations.Remove(reservation);
                }

                foreach (var menuItem in restaurant.MenuItems)
                {
                    context.OrderItems.RemoveRange(menuItem.OrderItems);
                    context.MenuItems.Remove(menuItem);
                }

                context.Tables.RemoveRange(restaurant.Tables);
                context.Restaurants.Remove(restaurant);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Restaurant");
            }
            else
            {
                Console.WriteLine("Restaurant not found.");
            }
        }
        static async Task DeleteTableAsync(RestaurantReservationDbContext context, int tableId)
        {
            var table = await context.Tables
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
                        context.OrderItems.RemoveRange(order.OrderItems);
                        context.Orders.Remove(order);
                    }
                    context.Reservations.Remove(reservation);
                }

                context.Tables.Remove(table);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Table");
            }
            else
            {
                Console.WriteLine("Table not found.");
            }
        }



    }
}