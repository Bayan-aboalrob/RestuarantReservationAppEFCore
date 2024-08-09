using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    public class Program
    {
        private static ServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Restaurant Reservation Application!");

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = _serviceProvider.CreateScope())
            {
                var customerRepository = scope.ServiceProvider.GetRequiredService<CustomerRepository>();
                var employeeRepository = scope.ServiceProvider.GetRequiredService<EmployeeRepository>();
                var menuItemRepository = scope.ServiceProvider.GetRequiredService<MenuItemRepository>();
                var orderRepository = scope.ServiceProvider.GetRequiredService<OrderRepository>();
                var orderItemRepository = scope.ServiceProvider.GetRequiredService<OrderItemRepository>();
                var reservationRepository = scope.ServiceProvider.GetRequiredService<ReservationRepository>();
                var restaurantRepository = scope.ServiceProvider.GetRequiredService<RestaurantRepository>();
                var tableRepository = scope.ServiceProvider.GetRequiredService<TableRepository>();

                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("\nSelect an operation:");
                    Console.WriteLine("1. List Managers");
                    Console.WriteLine("2. Get Reservations by Customer");
                    Console.WriteLine("3. List Orders and Menu Items by Reservation");
                    Console.WriteLine("4. List Ordered Menu Items by Reservation");
                    Console.WriteLine("5. Calculate Average Order Amount by Employee");
                    Console.WriteLine("6. List Reservations With Details");
                    Console.WriteLine("7. List Employees With Restaurant Details");
                    Console.WriteLine("8. Calculate Total Revenue by Restaurant");
                    Console.WriteLine("9. Get Customers With Large Reservations");
                    Console.WriteLine("10. Add Entity");
                    Console.WriteLine("11. Update Entity");
                    Console.WriteLine("12. Delete Entity");
                    Console.WriteLine("13. Exit");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await ListManagers(employeeRepository);
                            break;
                        case "2":
                            Console.Write("Enter Customer ID: ");
                            if (int.TryParse(Console.ReadLine(), out int customerId))
                            {
                                await GetReservationsByCustomer(customerRepository, customerId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer ID");
                            }
                            break;
                        case "3":
                            Console.Write("Enter Reservation ID: ");
                            if (int.TryParse(Console.ReadLine(), out int reservationId))
                            {
                                await ListOrdersAndMenuItems(orderRepository, reservationId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Reservation ID");
                            }
                            break;
                        case "4":
                            Console.Write("Enter Reservation ID: ");
                            if (int.TryParse(Console.ReadLine(), out int resId))
                            {
                                await ListOrderedMenuItems(orderRepository, resId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Reservation ID");
                            }
                            break;
                        case "5":
                            Console.Write("Enter Employee ID: ");
                            if (int.TryParse(Console.ReadLine(), out int employeeId))
                            {
                                await CalculateAverageOrderAmount(orderRepository, employeeId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Employee ID");
                            }
                            break;
                        case "6":
                            await ListReservationsWithDetails(reservationRepository);
                            break;
                        case "7":
                            await ListEmployeesWithRestaurantDetails(employeeRepository);
                            break;
                        case "8":
                            Console.Write("Enter Restaurant ID: ");
                            if (int.TryParse(Console.ReadLine(), out int restaurantId))
                            {
                                decimal totalRevenue = await CalculateTotalRevenueAsync(restaurantRepository, restaurantId);
                                Console.WriteLine($"Total Revenue for Restaurant {restaurantId}: {totalRevenue}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Restaurant ID");
                            }
                            break;
                        case "9":
                            Console.Write("Enter Minimum Party Size: ");
                            if (int.TryParse(Console.ReadLine(), out int partySize))
                            {
                                await GetCustomersWithLargeReservations(customerRepository, partySize);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Party Size");
                            }
                            break;
                        case "10":
                            await AddEntity(customerRepository, employeeRepository, menuItemRepository, orderRepository, orderItemRepository, reservationRepository, restaurantRepository, tableRepository);
                            break;
                        case "11":
                            await UpdateEntity(customerRepository, employeeRepository, menuItemRepository, orderRepository, orderItemRepository, reservationRepository, restaurantRepository, tableRepository);
                            break;
                        case "12":
                            await DeleteEntity(customerRepository, employeeRepository, menuItemRepository, orderRepository, orderItemRepository, reservationRepository, restaurantRepository, tableRepository);
                            break;
                        case "13":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestaurantReservationDbContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-HOSEMG4;Initial Catalog=RestuarantReservationCoreApp;Integrated Security=True;TrustServerCertificate=True;"));

            services.AddScoped<CustomerRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<MenuItemRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<OrderItemRepository>();
            services.AddScoped<ReservationRepository>();
            services.AddScoped<RestaurantRepository>();
            services.AddScoped<TableRepository>();
        }

        private static async Task ListManagers(EmployeeRepository employeeRepository)
        {
            var managers = await employeeRepository.ListManagersAsync();
            if (managers.Count > 0)
            {
                Console.WriteLine("Managers:");
                foreach (var manager in managers)
                {
                    Console.WriteLine($"{manager.FirstName} {manager.LastName}");
                }
            }
            else
            {
                Console.WriteLine("No managers found.");
            }
        }

        private static async Task GetReservationsByCustomer(CustomerRepository customerRepository, int customerId)
        {
            var reservations = await customerRepository.GetReservationsByCustomerAsync(customerId);
            if (reservations.Count > 0)
            {
                Console.WriteLine($"Reservations for customer {customerId}:");
                foreach (var reservation in reservations)
                {
                    Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Date: {reservation.ReservationDate}, Party Size: {reservation.PartySize}");
                }
            }
            else
            {
                Console.WriteLine($"No reservations found for customer {customerId}.");
            }
        }

        private static async Task ListOrdersAndMenuItems(OrderRepository orderRepository, int reservationId)
        {
            var orders = await orderRepository.ListOrdersAndMenuItemsAsync(reservationId);
            if (orders.Count > 0)
            {
                Console.WriteLine($"Orders and Menu Items for reservation {reservationId}:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Total Amount: {order.TotalAmount}");
                    foreach (var orderItem in order.OrderItems)
                    {
                        Console.WriteLine($"Menu Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"No orders found for reservation {reservationId}.");
            }
        }

        private static async Task ListOrderedMenuItems(OrderRepository orderRepository, int reservationId)
        {
            var orderedMenuItems = await orderRepository.ListOrderedMenuItemsAsync(reservationId);
            if (orderedMenuItems.Count > 0)
            {
                Console.WriteLine($"Ordered Menu Items for reservation {reservationId}:");
                foreach (var orderItem in orderedMenuItems)
                {
                    Console.WriteLine($"Menu Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
                }
            }
            else
            {
                Console.WriteLine($"No menu items found for reservation {reservationId}.");
            }
        }

        private static async Task CalculateAverageOrderAmount(OrderRepository orderRepository, int employeeId)
        {
            var averageOrderAmount = await orderRepository.CalculateAverageOrderAmountAsync(employeeId);
            Console.WriteLine($"Average Order Amount for employee {employeeId}: {averageOrderAmount}");
        }

        private static async Task ListReservationsWithDetails(ReservationRepository reservationRepository)
        {
            var reservations = await reservationRepository.ListReservationsWithDetailsAsync();
            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
            }
            else
            {
                Console.WriteLine("Reservations with details:");
                foreach (var reservation in reservations)
                {
                    Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Date: {reservation.ReservationDate}, " +
                                      $"Party Size: {reservation.PartySize}, Customer: {reservation.CustomerFirstName} {reservation.CustomerLastName}, " +
                                      $"Email: {reservation.CustomerEmail}, Restaurant: {reservation.RestaurantName}, " +
                                      $"Address: {reservation.RestaurantAddress}, Phone: {reservation.RestaurantPhoneNumber}");
                }
            }
        }

        private static async Task ListEmployeesWithRestaurantDetails(EmployeeRepository employeeRepository)
        {
            var employees = await employeeRepository.ListEmployeesWithRestaurantDetailsAsync();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                Console.WriteLine("Employees with restaurant details:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, " +
                                      $"Position: {employee.Position}, Restaurant: {employee.RestaurantName}, " +
                                      $"Address: {employee.RestaurantAddress}, Phone: {employee.RestaurantPhoneNumber}");
                }
            }
        }

        private static async Task GetCustomersWithLargeReservations(CustomerRepository customerRepository, int partySize)
        {
            var customers = await customerRepository.GetCustomersWithLargeReservationsAsync(partySize);
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found with large reservations.");
            }
            else
            {
                Console.WriteLine("Customers with large reservations:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
                }
            }
        }

        private static async Task<decimal> CalculateTotalRevenueAsync(RestaurantRepository restaurantRepository, int restaurantId)
        {
            var totalRevenue = await restaurantRepository.CalculateTotalRevenueAsync(restaurantId);
            return totalRevenue;
        }

        private static async Task AddEntity(
            CustomerRepository customerRepository,
            EmployeeRepository employeeRepository,
            MenuItemRepository menuItemRepository,
            OrderRepository orderRepository,
            OrderItemRepository orderItemRepository,
            ReservationRepository reservationRepository,
            RestaurantRepository restaurantRepository,
            TableRepository tableRepository)
        {
            Console.WriteLine("\nSelect an entity to add:");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Employee");
            Console.WriteLine("3. MenuItem");
            Console.WriteLine("4. Order");
            Console.WriteLine("5. OrderItem");
            Console.WriteLine("6. Reservation");
            Console.WriteLine("7. Restaurant");
            Console.WriteLine("8. Table");
            Console.WriteLine("9. Cancel");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddCustomerAsync(customerRepository);
                    break;
                case "2":
                    await AddEmployeeAsync(employeeRepository);
                    break;
                case "3":
                    await AddMenuItemAsync(menuItemRepository);
                    break;
                case "4":
                    await AddOrderAsync(orderRepository);
                    break;
                case "5":
                    await AddOrderItemAsync(orderItemRepository);
                    break;
                case "6":
                    await AddReservationAsync(reservationRepository);
                    break;
                case "7":
                    await AddRestaurantAsync(restaurantRepository);
                    break;
                case "8":
                    await AddTableAsync(tableRepository);
                    break;
                case "9":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static async Task UpdateEntity(
            CustomerRepository customerRepository,
            EmployeeRepository employeeRepository,
            MenuItemRepository menuItemRepository,
            OrderRepository orderRepository,
            OrderItemRepository orderItemRepository,
            ReservationRepository reservationRepository,
            RestaurantRepository restaurantRepository,
            TableRepository tableRepository)
        {
            Console.WriteLine("\nSelect an entity to update:");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Employee");
            Console.WriteLine("3. MenuItem");
            Console.WriteLine("4. Order");
            Console.WriteLine("5. OrderItem");
            Console.WriteLine("6. Reservation");
            Console.WriteLine("7. Restaurant");
            Console.WriteLine("8. Table");
            Console.WriteLine("9. Cancel");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await UpdateCustomerAsync(customerRepository);
                    break;
                case "2":
                    await UpdateEmployeeAsync(employeeRepository);
                    break;
                case "3":
                    await UpdateMenuItemAsync(menuItemRepository);
                    break;
                case "4":
                    await UpdateOrderAsync(orderRepository);
                    break;
                case "5":
                    await UpdateOrderItemAsync(orderItemRepository);
                    break;
                case "6":
                    await UpdateReservationAsync(reservationRepository);
                    break;
                case "7":
                    await UpdateRestaurantAsync(restaurantRepository);
                    break;
                case "8":
                    await UpdateTableAsync(tableRepository);
                    break;
                case "9":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static async Task DeleteEntity(
            CustomerRepository customerRepository,
            EmployeeRepository employeeRepository,
            MenuItemRepository menuItemRepository,
            OrderRepository orderRepository,
            OrderItemRepository orderItemRepository,
            ReservationRepository reservationRepository,
            RestaurantRepository restaurantRepository,
            TableRepository tableRepository)
        {
            Console.WriteLine("\nSelect an entity to delete:");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Employee");
            Console.WriteLine("3. MenuItem");
            Console.WriteLine("4. Order");
            Console.WriteLine("5. OrderItem");
            Console.WriteLine("6. Reservation");
            Console.WriteLine("7. Restaurant");
            Console.WriteLine("8. Table");
            Console.WriteLine("9. Cancel");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await DeleteCustomerAsync(customerRepository);
                    break;
                case "2":
                    await DeleteEmployeeAsync(employeeRepository);
                    break;
                case "3":
                    await DeleteMenuItemAsync(menuItemRepository);
                    break;
                case "4":
                    await DeleteOrderAsync(orderRepository);
                    break;
                case "5":
                    await DeleteOrderItemAsync(orderItemRepository);
                    break;
                case "6":
                    await DeleteReservationAsync(reservationRepository);
                    break;
                case "7":
                    await DeleteRestaurantAsync(restaurantRepository);
                    break;
                case "8":
                    await DeleteTableAsync(tableRepository);
                    break;
                case "9":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static async Task AddCustomerAsync(CustomerRepository customerRepository)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            await customerRepository.AddCustomerAsync(newCustomer);
            Console.WriteLine("Customer added successfully.");
        }

        private static async Task AddEmployeeAsync(EmployeeRepository employeeRepository)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Position: ");
            string position = Console.ReadLine();
            Console.Write("Enter Restaurant ID: ");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                var newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Position = position,
                    RestaurantId = restaurantId
                };

                await employeeRepository.AddEmployeeAsync(newEmployee);
                Console.WriteLine("Employee added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Restaurant ID");
            }
        }

        private static async Task AddMenuItemAsync(MenuItemRepository menuItemRepository)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.Write("Enter Restaurant ID: ");
                if (int.TryParse(Console.ReadLine(), out int restaurantId))
                {
                    var newMenuItem = new MenuItem
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        RestaurantId = restaurantId
                    };

                    await menuItemRepository.AddMenuItemAsync(newMenuItem);
                    Console.WriteLine("Menu item added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid Restaurant ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Price");
            }
        }

        private static async Task AddOrderAsync(OrderRepository orderRepository)
        {
            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.Write("Enter Reservation ID: ");
                if (int.TryParse(Console.ReadLine(), out int reservationId))
                {
                    Console.Write("Enter Total Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal totalAmount))
                    {
                        var newOrder = new Order
                        {
                            EmployeeId = employeeId,
                            ReservationId = reservationId,
                            OrderDate = DateTime.Now,
                            TotalAmount = totalAmount
                        };

                        await orderRepository.AddOrderAsync(newOrder);
                        Console.WriteLine("Order added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Total Amount");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Reservation ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID");
            }
        }

        private static async Task AddOrderItemAsync(OrderItemRepository orderItemRepository)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.Write("Enter Menu Item ID: ");
                if (int.TryParse(Console.ReadLine(), out int itemId))
                {
                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        var newOrderItem = new OrderItem
                        {
                            OrderId = orderId,
                            ItemId = itemId,
                            Quantity = quantity
                        };

                        await orderItemRepository.AddOrderItemAsync(newOrderItem);
                        Console.WriteLine("Order item added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Quantity");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Menu Item ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID");
            }
        }

        private static async Task AddReservationAsync(ReservationRepository reservationRepository)
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.Write("Enter Restaurant ID: ");
                if (int.TryParse(Console.ReadLine(), out int restaurantId))
                {
                    Console.Write("Enter Table ID: ");
                    if (int.TryParse(Console.ReadLine(), out int tableId))
                    {
                        Console.Write("Enter Party Size: ");
                        if (int.TryParse(Console.ReadLine(), out int partySize))
                        {
                            var newReservation = new Reservation
                            {
                                CustomerId = customerId,
                                RestaurantId = restaurantId,
                                TableId = tableId,
                                PartySize = partySize,
                                ReservationDate = DateTime.Now
                            };

                            await reservationRepository.AddReservationAsync(newReservation);
                            Console.WriteLine("Reservation added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Party Size");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Table ID");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Restaurant ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Customer ID");
            }
        }

        private static async Task AddRestaurantAsync(RestaurantRepository restaurantRepository)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter Opening Hours: ");
            string openingHours = Console.ReadLine();

            var newRestaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                OpeningHours = openingHours
            };

            await restaurantRepository.AddRestaurantAsync(newRestaurant);
            Console.WriteLine("Restaurant added successfully.");
        }

        private static async Task AddTableAsync(TableRepository tableRepository)
        {
            Console.Write("Enter Capacity: ");
            if (int.TryParse(Console.ReadLine(), out int capacity))
            {
                Console.Write("Enter Restaurant ID: ");
                if (int.TryParse(Console.ReadLine(), out int restaurantId))
                {
                    var newTable = new Table
                    {
                        Capacity = capacity,
                        RestaurantId = restaurantId
                    };

                    await tableRepository.AddTableAsync(newTable);
                    Console.WriteLine("Table added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid Restaurant ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Capacity");
            }
        }

        private static async Task UpdateCustomerAsync(CustomerRepository customerRepository)
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.Write("Enter New First Name: ");
                string newFirstName = Console.ReadLine();
                Console.Write("Enter New Last Name: ");
                string newLastName = Console.ReadLine();
                Console.Write("Enter New Email: ");
                string newEmail = Console.ReadLine();
                Console.Write("Enter New Phone Number: ");
                string newPhoneNumber = Console.ReadLine();

                await customerRepository.UpdateCustomerAsync(customerId, newFirstName, newLastName, newEmail, newPhoneNumber);
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Customer ID");
            }
        }

        private static async Task UpdateEmployeeAsync(EmployeeRepository employeeRepository)
        {
            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.Write("Enter New First Name: ");
                string newFirstName = Console.ReadLine();
                Console.Write("Enter New Last Name: ");
                string newLastName = Console.ReadLine();
                Console.Write("Enter New Position: ");
                string newPosition = Console.ReadLine();
                Console.Write("Enter New Restaurant ID: ");
                if (int.TryParse(Console.ReadLine(), out int newRestaurantId))
                {
                    await employeeRepository.UpdateEmployeeAsync(employeeId, newFirstName, newLastName, newPosition, newRestaurantId);
                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid Restaurant ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID");
            }
        }

        private static async Task UpdateMenuItemAsync(MenuItemRepository menuItemRepository)
        {
            Console.Write("Enter Menu Item ID: ");
            if (int.TryParse(Console.ReadLine(), out int menuItemId))
            {
                Console.Write("Enter New Name: ");
                string newName = Console.ReadLine();
                Console.Write("Enter New Description: ");
                string newDescription = Console.ReadLine();
                Console.Write("Enter New Price: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                {
                    Console.Write("Enter New Restaurant ID: ");
                    if (int.TryParse(Console.ReadLine(), out int newRestaurantId))
                    {
                        await menuItemRepository.UpdateMenuItemAsync(menuItemId, newName, newDescription, newPrice, newRestaurantId);
                        Console.WriteLine("Menu item updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Restaurant ID");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Price");
                }
            }
            else
            {
                Console.WriteLine("Invalid Menu Item ID");
            }
        }

        private static async Task UpdateOrderAsync(OrderRepository orderRepository)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.Write("Enter New Order Date (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newOrderDate))
                {
                    Console.Write("Enter New Total Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newTotalAmount))
                    {
                        Console.Write("Enter New Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newEmployeeId))
                        {
                            Console.Write("Enter New Reservation ID: ");
                            if (int.TryParse(Console.ReadLine(), out int newReservationId))
                            {
                                await orderRepository.UpdateOrderAsync(orderId, newOrderDate, newTotalAmount, newEmployeeId, newReservationId);
                                Console.WriteLine("Order updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Reservation ID");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Total Amount");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Order Date");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID");
            }
        }

        private static async Task UpdateOrderItemAsync(OrderItemRepository orderItemRepository)
        {
            Console.Write("Enter Order Item ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderItemId))
            {
                Console.Write("Enter New Menu Item ID: ");
                if (int.TryParse(Console.ReadLine(), out int newItemId))
                {
                    Console.Write("Enter New Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int newQuantity))
                    {
                        Console.Write("Enter New Order ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newOrderId))
                        {
                            await orderItemRepository.UpdateOrderItemAsync(orderItemId, newItemId, newQuantity, newOrderId);
                            Console.WriteLine("Order item updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Quantity");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Menu Item ID");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order Item ID");
            }
        }

        private static async Task UpdateReservationAsync(ReservationRepository reservationRepository)
        {
            Console.Write("Enter Reservation ID: ");
            if (int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.Write("Enter New Reservation Date (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newReservationDate))
                {
                    Console.Write("Enter New Party Size: ");
                    if (int.TryParse(Console.ReadLine(), out int newPartySize))
                    {
                        Console.Write("Enter New Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newCustomerId))
                        {
                            Console.Write("Enter New Restaurant ID: ");
                            if (int.TryParse(Console.ReadLine(), out int newRestaurantId))
                            {
                                Console.Write("Enter New Table ID: ");
                                if (int.TryParse(Console.ReadLine(), out int newTableId))
                                {
                                    await reservationRepository.UpdateReservationAsync(reservationId, newReservationDate, newPartySize, newCustomerId, newRestaurantId, newTableId);
                                    Console.WriteLine("Reservation updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Table ID");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Restaurant ID");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Customer ID");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Party Size");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Reservation Date");
                }
            }
            else
            {
                Console.WriteLine("Invalid Reservation ID");
            }
        }

        private static async Task UpdateRestaurantAsync(RestaurantRepository restaurantRepository)
        {
            Console.Write("Enter Restaurant ID: ");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                Console.Write("Enter New Name: ");
                string newName = Console.ReadLine();
                Console.Write("Enter New Address: ");
                string newAddress = Console.ReadLine();
                Console.Write("Enter New Phone Number: ");
                string newPhoneNumber = Console.ReadLine();
                Console.Write("Enter New Opening Hours: ");
                string newOpeningHours = Console.ReadLine();

                await restaurantRepository.UpdateRestaurantAsync(restaurantId, newName, newAddress, newPhoneNumber, newOpeningHours);
                Console.WriteLine("Restaurant updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Restaurant ID");
            }
        }

        private static async Task UpdateTableAsync(TableRepository tableRepository)
        {
            Console.Write("Enter Table ID: ");
            if (int.TryParse(Console.ReadLine(), out int tableId))
            {
                Console.Write("Enter New Capacity: ");
                if (int.TryParse(Console.ReadLine(), out int newCapacity))
                {
                    Console.Write("Enter New Restaurant ID: ");
                    if (int.TryParse(Console.ReadLine(), out int newRestaurantId))
                    {
                        await tableRepository.UpdateTableAsync(tableId, newCapacity, newRestaurantId);
                        Console.WriteLine("Table updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Restaurant ID");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Capacity");
                }
            }
            else
            {
                Console.WriteLine("Invalid Table ID");
            }
        }

        private static async Task DeleteCustomerAsync(CustomerRepository customerRepository)
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                await customerRepository.DeleteCustomerAsync(customerId);
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Customer ID");
            }
        }

        private static async Task DeleteEmployeeAsync(EmployeeRepository employeeRepository)
        {
            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                await employeeRepository.DeleteEmployeeAsync(employeeId);
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Employee ID");
            }
        }

        private static async Task DeleteMenuItemAsync(MenuItemRepository menuItemRepository)
        {
            Console.Write("Enter Menu Item ID: ");
            if (int.TryParse(Console.ReadLine(), out int menuItemId))
            {
                await menuItemRepository.DeleteMenuItemAsync(menuItemId);
                Console.WriteLine("Menu item deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Menu Item ID");
            }
        }

        private static async Task DeleteOrderAsync(OrderRepository orderRepository)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                await orderRepository.DeleteOrderAsync(orderId);
                Console.WriteLine("Order deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Order ID");
            }
        }

        private static async Task DeleteOrderItemAsync(OrderItemRepository orderItemRepository)
        {
            Console.Write("Enter Order Item ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderItemId))
            {
                await orderItemRepository.DeleteOrderItemAsync(orderItemId);
                Console.WriteLine("Order item deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Order Item ID");
            }
        }

        private static async Task DeleteReservationAsync(ReservationRepository reservationRepository)
        {
            Console.Write("Enter Reservation ID: ");
            if (int.TryParse(Console.ReadLine(), out int reservationId))
            {
                await reservationRepository.DeleteReservationAsync(reservationId);
                Console.WriteLine("Reservation deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Reservation ID");
            }
        }

        private static async Task DeleteRestaurantAsync(RestaurantRepository restaurantRepository)
        {
            Console.Write("Enter Restaurant ID: ");
            if (int.TryParse(Console.ReadLine(), out int restaurantId))
            {
                await restaurantRepository.DeleteRestaurantAsync(restaurantId);
                Console.WriteLine("Restaurant deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Restaurant ID");
            }
        }

        private static async Task DeleteTableAsync(TableRepository tableRepository)
        {
            Console.Write("Enter Table ID: ");
            if (int.TryParse(Console.ReadLine(), out int tableId))
            {
                await tableRepository.DeleteTableAsync(tableId);
                Console.WriteLine("Table deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Table ID");
            }
        }
    }
}
