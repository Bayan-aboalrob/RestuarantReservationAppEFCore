﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    [DbContext(typeof(RestaurantReservationDbContext))]
    partial class RestaurantReservationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RestaurantReservation.Db.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "b.aboalrob@example.com",
                            FirstName = "Bayan",
                            LastName = "Aboalrob",
                            PhoneNumber = "111-111-1111"
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "Soso.smith@example.com",
                            FirstName = "Soso",
                            LastName = "Smith",
                            PhoneNumber = "222-222-2222"
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "bob.johnson@example.com",
                            FirstName = "Bob",
                            LastName = "Johnson",
                            PhoneNumber = "333-333-3333"
                        },
                        new
                        {
                            CustomerId = 4,
                            Email = "Omar.Salim@example.com",
                            FirstName = "Omar",
                            LastName = "Salem",
                            PhoneNumber = "444-444-4444"
                        },
                        new
                        {
                            CustomerId = 5,
                            Email = "tom.brown@example.com",
                            FirstName = "Tom",
                            LastName = "Brown",
                            PhoneNumber = "555-555-5555"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            FirstName = "Mark",
                            LastName = "Spencer",
                            Position = "Manager",
                            RestaurantId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            FirstName = "Sara",
                            LastName = "Connor",
                            Position = "Chef",
                            RestaurantId = 2
                        },
                        new
                        {
                            EmployeeId = 3,
                            FirstName = "Paul",
                            LastName = "Walker",
                            Position = "Waiter",
                            RestaurantId = 3
                        },
                        new
                        {
                            EmployeeId = 4,
                            FirstName = "Laura",
                            LastName = "Bush",
                            Position = "Manager",
                            RestaurantId = 4
                        },
                        new
                        {
                            EmployeeId = 5,
                            FirstName = "Peter",
                            LastName = "Parker",
                            Position = "Waiter",
                            RestaurantId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.MenuItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Description = "Cheese Pizza",
                            Name = "Pizza",
                            Price = 9.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            ItemId = 2,
                            Description = "Beef Burger",
                            Name = "Burger",
                            Price = 11.99m,
                            RestaurantId = 2
                        },
                        new
                        {
                            ItemId = 3,
                            Description = "Spaghetti Bolognese",
                            Name = "Pasta",
                            Price = 12.99m,
                            RestaurantId = 3
                        },
                        new
                        {
                            ItemId = 4,
                            Description = "Grilled Steak",
                            Name = "Steak",
                            Price = 19.99m,
                            RestaurantId = 4
                        },
                        new
                        {
                            ItemId = 5,
                            Description = "Grilled Lobster",
                            Name = "Lobster",
                            Price = 29.99m,
                            RestaurantId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 7, 5, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5736),
                            ReservationId = 1,
                            TotalAmount = 39.96m
                        },
                        new
                        {
                            OrderId = 2,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 7, 5, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5738),
                            ReservationId = 2,
                            TotalAmount = 23.98m
                        },
                        new
                        {
                            OrderId = 3,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 7, 5, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5740),
                            ReservationId = 3,
                            TotalAmount = 77.94m
                        },
                        new
                        {
                            OrderId = 4,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 7, 5, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5742),
                            ReservationId = 4,
                            TotalAmount = 79.96m
                        },
                        new
                        {
                            OrderId = 5,
                            EmployeeId = 5,
                            OrderDate = new DateTime(2024, 7, 5, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5744),
                            ReservationId = 5,
                            TotalAmount = 149.95m
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            ItemId = 1,
                            OrderId = 1,
                            Quantity = 4
                        },
                        new
                        {
                            OrderItemId = 2,
                            ItemId = 2,
                            OrderId = 2,
                            Quantity = 2
                        },
                        new
                        {
                            OrderItemId = 3,
                            ItemId = 3,
                            OrderId = 3,
                            Quantity = 6
                        },
                        new
                        {
                            OrderItemId = 4,
                            ItemId = 4,
                            OrderId = 4,
                            Quantity = 4
                        },
                        new
                        {
                            OrderItemId = 5,
                            ItemId = 5,
                            OrderId = 5,
                            Quantity = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("PartySize")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            ReservationId = 1,
                            CustomerId = 1,
                            PartySize = 4,
                            ReservationDate = new DateTime(2024, 7, 6, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5683),
                            RestaurantId = 1,
                            TableId = 1
                        },
                        new
                        {
                            ReservationId = 2,
                            CustomerId = 2,
                            PartySize = 2,
                            ReservationDate = new DateTime(2024, 7, 7, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5719),
                            RestaurantId = 2,
                            TableId = 3
                        },
                        new
                        {
                            ReservationId = 3,
                            CustomerId = 3,
                            PartySize = 6,
                            ReservationDate = new DateTime(2024, 7, 8, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5721),
                            RestaurantId = 3,
                            TableId = 4
                        },
                        new
                        {
                            ReservationId = 4,
                            CustomerId = 4,
                            PartySize = 4,
                            ReservationDate = new DateTime(2024, 7, 9, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5723),
                            RestaurantId = 4,
                            TableId = 5
                        },
                        new
                        {
                            ReservationId = 5,
                            CustomerId = 5,
                            PartySize = 5,
                            ReservationDate = new DateTime(2024, 7, 10, 14, 44, 3, 443, DateTimeKind.Local).AddTicks(5725),
                            RestaurantId = 5,
                            TableId = 6
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RestaurantId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            Address = "Jenin",
                            Name = "Ali Baba",
                            OpeningHours = "8 AM - 10 PM",
                            PhoneNumber = "123-456-7890"
                        },
                        new
                        {
                            RestaurantId = 2,
                            Address = "Nablus",
                            Name = "Pizza Place",
                            OpeningHours = "10 AM - 12 AM",
                            PhoneNumber = "987-654-3210"
                        },
                        new
                        {
                            RestaurantId = 3,
                            Address = "Ramallah",
                            Name = "Burger Joint",
                            OpeningHours = "11 AM - 11 PM",
                            PhoneNumber = "555-555-5555"
                        },
                        new
                        {
                            RestaurantId = 4,
                            Address = "Jerusalem",
                            Name = "Fine Dine",
                            OpeningHours = "12 PM - 12 AM",
                            PhoneNumber = "111-222-3333"
                        },
                        new
                        {
                            RestaurantId = 5,
                            Address = "Jaffa",
                            Name = "Seafood Delight",
                            OpeningHours = "1 PM - 11 PM",
                            PhoneNumber = "444-555-6666"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("TableId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            TableId = 1,
                            Capacity = 4,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 2,
                            Capacity = 2,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 3,
                            Capacity = 6,
                            RestaurantId = 2
                        },
                        new
                        {
                            TableId = 4,
                            Capacity = 4,
                            RestaurantId = 3
                        },
                        new
                        {
                            TableId = 5,
                            Capacity = 8,
                            RestaurantId = 4
                        },
                        new
                        {
                            TableId = 6,
                            Capacity = 5,
                            RestaurantId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Employee", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Restaurant", "Restaurant")
                        .WithMany("Employees")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.MenuItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Restaurant", "Restaurant")
                        .WithMany("MenuItems")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Order", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Reservation", "Reservation")
                        .WithMany("Orders")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RestaurantReservation.Db.OrderItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Reservation", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Table", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Reservation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Restaurant", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("MenuItems");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
