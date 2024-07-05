using Microsoft.EntityFrameworkCore;
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
                    FirstName = "Sali",
                    LastName = "Dolani",
                    Email = "Sara.Dolani@example.com",
                    PhoneNumber = "123-456-1234"
                };

                await AddCustomerAsync(context, newCustomer);

                var newRestaurant = new Restaurant { Name = "Ali Baba Restaurant 2", Address = "Jenin-Ramallah", PhoneNumber = "123-456-0236", OpeningHours = "7 AM - 12 PM" };
                await AddRestaurantAsync(context, newRestaurant);

                var newTable = new Table { Capacity = 12, RestaurantId = newRestaurant.RestaurantId };
                await AddTableAsync(context, newTable);

                var newEmployee = new Employee { FirstName = "Omar", LastName = "Aboalrob", Position = "Manager", RestaurantId = newRestaurant.RestaurantId };
                await AddEmployeeAsync(context, newEmployee);

                var newMenuItem = new MenuItem { Name = "Mushroom White Sauce", Description = "*2 Italian", Price = 55.2m, RestaurantId = newRestaurant.RestaurantId };
                await AddMenuItemAsync(context, newMenuItem);

                var newReservation = new Reservation { CustomerId = newCustomer.CustomerId, RestaurantId = newRestaurant.RestaurantId, TableId = newTable.TableId, ReservationDate = DateTime.Now.AddDays(1), PartySize = 30 };
                await AddReservationAsync(context, newReservation);

                var newOrder = new Order { EmployeeId = newEmployee.EmployeeId, OrderDate = DateTime.Now, TotalAmount = 39.96m, ReservationId = newReservation.ReservationId };
                await AddOrderAsync(context, newOrder);

                var newOrderItem = new OrderItem { ItemId = newMenuItem.ItemId, Quantity = 56, OrderId = newOrder.OrderId };
                await AddOrderItemAsync(context, newOrderItem);
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

    }
}