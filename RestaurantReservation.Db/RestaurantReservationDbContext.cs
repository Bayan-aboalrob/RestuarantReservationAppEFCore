using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<ReservationWithDetails> ReservationsWithDetails { get; set; }
        public DbSet<EmployeeWithRestaurantDetails> EmployeesWithRestaurantDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Data Source=DESKTOP-HOSEMG4;Initial Catalog=RestuarantReservationCoreApp;Integrated Security=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RestaurantId);

            modelBuilder.Entity<MenuItem>()
                .HasKey(m => m.ItemId);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .IsRequired();

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Reservation)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.ReservationId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.ReservationId);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ReservationDate)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.PartySize)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(re => re.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Restaurant>()
                .HasKey(r => r.RestaurantId);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Address)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<Restaurant>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.OpeningHours)
                .IsRequired();

            modelBuilder.Entity<Table>()
                .HasKey(t => t.TableId);

            modelBuilder.Entity<Table>()
                .Property(t => t.Capacity)
                .IsRequired();

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.RestaurantId);


            // Seeding each table with 5 records

            // Seeding data
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { RestaurantId = 1, Name = "Ali Baba", Address = "Jenin", PhoneNumber = "123-456-7890", OpeningHours = "8 AM - 10 PM" },
                new Restaurant { RestaurantId = 2, Name = "Pizza Place", Address = "Nablus", PhoneNumber = "987-654-3210", OpeningHours = "10 AM - 12 AM" },
                new Restaurant { RestaurantId = 3, Name = "Burger Joint", Address = "Ramallah", PhoneNumber = "555-555-5555", OpeningHours = "11 AM - 11 PM" },
                new Restaurant { RestaurantId = 4, Name = "Fine Dine", Address = "Jerusalem", PhoneNumber = "111-222-3333", OpeningHours = "12 PM - 12 AM" },
                new Restaurant { RestaurantId = 5, Name = "Seafood Delight", Address = "Jaffa", PhoneNumber = "444-555-6666", OpeningHours = "1 PM - 11 PM" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Bayan", LastName = "Aboalrob", Email = "b.aboalrob@example.com", PhoneNumber = "111-111-1111" },
                new Customer { CustomerId = 2, FirstName = "Soso", LastName = "Smith", Email = "Soso.smith@example.com", PhoneNumber = "222-222-2222" },
                new Customer { CustomerId = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", PhoneNumber = "333-333-3333" },
                new Customer { CustomerId = 4, FirstName = "Omar", LastName = "Salem", Email = "Omar.Salim@example.com", PhoneNumber = "444-444-4444" },
                new Customer { CustomerId = 5, FirstName = "Tom", LastName = "Brown", Email = "tom.brown@example.com", PhoneNumber = "555-555-5555" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "Mark", LastName = "Spencer", Position = "Manager", RestaurantId = 1 },
                new Employee { EmployeeId = 2, FirstName = "Sara", LastName = "Connor", Position = "Chef", RestaurantId = 2 },
                new Employee { EmployeeId = 3, FirstName = "Paul", LastName = "Walker", Position = "Waiter", RestaurantId = 3 },
                new Employee { EmployeeId = 4, FirstName = "Laura", LastName = "Bush", Position = "Manager", RestaurantId = 4 },
                new Employee { EmployeeId = 5, FirstName = "Peter", LastName = "Parker", Position = "Waiter", RestaurantId = 5 }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, Capacity = 4, RestaurantId = 1 },
                new Table { TableId = 2, Capacity = 2, RestaurantId = 1 },
                new Table { TableId = 3, Capacity = 6, RestaurantId = 2 },
                new Table { TableId = 4, Capacity = 4, RestaurantId = 3 },
                new Table { TableId = 5, Capacity = 8, RestaurantId = 4 },
                new Table { TableId = 6, Capacity = 5, RestaurantId = 5 }
            );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { ItemId = 1, Name = "Pizza", Description = "Cheese Pizza", Price = 9.99m, RestaurantId = 1 },
                new MenuItem { ItemId = 2, Name = "Burger", Description = "Beef Burger", Price = 11.99m, RestaurantId = 2 },
                new MenuItem { ItemId = 3, Name = "Pasta", Description = "Spaghetti Bolognese", Price = 12.99m, RestaurantId = 3 },
                new MenuItem { ItemId = 4, Name = "Steak", Description = "Grilled Steak", Price = 19.99m, RestaurantId = 4 },
                new MenuItem { ItemId = 5, Name = "Lobster", Description = "Grilled Lobster", Price = 29.99m, RestaurantId = 5 }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, RestaurantId = 1, ReservationDate = DateTime.Now.AddDays(1), PartySize = 4, CustomerId = 1, TableId = 1 },
                new Reservation { ReservationId = 2, RestaurantId = 2, ReservationDate = DateTime.Now.AddDays(2), PartySize = 2, CustomerId = 2, TableId = 3 },
                new Reservation { ReservationId = 3, RestaurantId = 3, ReservationDate = DateTime.Now.AddDays(3), PartySize = 6, CustomerId = 3, TableId = 4 },
                new Reservation { ReservationId = 4, RestaurantId = 4, ReservationDate = DateTime.Now.AddDays(4), PartySize = 4, CustomerId = 4, TableId = 5 },
                new Reservation { ReservationId = 5, RestaurantId = 5, ReservationDate = DateTime.Now.AddDays(5), PartySize = 5, CustomerId = 5, TableId = 6 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, EmployeeId = 1, OrderDate = DateTime.Now, TotalAmount = 39.96m, ReservationId = 1 },
                new Order { OrderId = 2, EmployeeId = 2, OrderDate = DateTime.Now, TotalAmount = 23.98m, ReservationId = 2 },
                new Order { OrderId = 3, EmployeeId = 3, OrderDate = DateTime.Now, TotalAmount = 77.94m, ReservationId = 3 },
                new Order { OrderId = 4, EmployeeId = 4, OrderDate = DateTime.Now, TotalAmount = 79.96m, ReservationId = 4 },
                new Order { OrderId = 5, EmployeeId = 5, OrderDate = DateTime.Now, TotalAmount = 149.95m, ReservationId = 5 }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, ItemId = 1, Quantity = 4, OrderId = 1 },
                new OrderItem { OrderItemId = 2, ItemId = 2, Quantity = 2, OrderId = 2 },
                new OrderItem { OrderItemId = 3, ItemId = 3, Quantity = 6, OrderId = 3 },
                new OrderItem { OrderItemId = 4, ItemId = 4, Quantity = 4, OrderId = 4 },
                new OrderItem { OrderItemId = 5, ItemId = 5, Quantity = 5, OrderId = 5 }
            );
            modelBuilder.Entity<ReservationWithDetails>().HasNoKey().ToView("ReservationsWithDetails");
            modelBuilder.Entity<EmployeeWithRestaurantDetails>().HasNoKey().ToView("EmployeesWithRestaurantDetails");


            base.OnModelCreating(modelBuilder);
        }
    }
}
