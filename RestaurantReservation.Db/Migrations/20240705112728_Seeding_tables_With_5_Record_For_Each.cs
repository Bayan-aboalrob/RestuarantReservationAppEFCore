using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    public partial class Seeding_tables_With_5_Record_For_Each : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "b.aboalrob@example.com", "Bayan", "Aboalrob", "111-111-1111" },
                    { 2, "Soso.smith@example.com", "Soso", "Smith", "222-222-2222" },
                    { 3, "bob.johnson@example.com", "Bob", "Johnson", "333-333-3333" },
                    { 4, "Omar.Salim@example.com", "Omar", "Salem", "444-444-4444" },
                    { 5, "tom.brown@example.com", "Tom", "Brown", "555-555-5555" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "Address", "Name", "OpeningHours", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Jenin", "Ali Baba", "8 AM - 10 PM", "123-456-7890" },
                    { 2, "Nablus", "Pizza Place", "10 AM - 12 AM", "987-654-3210" },
                    { 3, "Ramallah", "Burger Joint", "11 AM - 11 PM", "555-555-5555" },
                    { 4, "Jerusalem", "Fine Dine", "12 PM - 12 AM", "111-222-3333" },
                    { 5, "Jaffa", "Seafood Delight", "1 PM - 11 PM", "444-555-6666" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "FirstName", "LastName", "Position", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Mark", "Spencer", "Manager", 1 },
                    { 2, "Sara", "Connor", "Chef", 2 },
                    { 3, "Paul", "Walker", "Waiter", 3 },
                    { 4, "Laura", "Bush", "Manager", 4 },
                    { 5, "Peter", "Parker", "Waiter", 5 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "ItemId", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Cheese Pizza", "Pizza", 9.99m, 1 },
                    { 2, "Beef Burger", "Burger", 11.99m, 2 },
                    { 3, "Spaghetti Bolognese", "Pasta", 12.99m, 3 },
                    { 4, "Grilled Steak", "Steak", 19.99m, 4 },
                    { 5, "Grilled Lobster", "Lobster", 29.99m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 1 },
                    { 3, 6, 2 },
                    { 4, 4, 3 },
                    { 5, 8, 4 },
                    { 6, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CustomerId", "PartySize", "ReservationDate", "RestaurantId", "TableId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2024, 7, 6, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1256), 1, 1 },
                    { 2, 2, 2, new DateTime(2024, 7, 7, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1303), 2, 3 },
                    { 3, 3, 6, new DateTime(2024, 7, 8, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1305), 3, 4 },
                    { 4, 4, 4, new DateTime(2024, 7, 9, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1308), 4, 5 },
                    { 5, 5, 5, new DateTime(2024, 7, 10, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1309), 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "EmployeeId", "OrderDate", "ReservationId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 5, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1327), 1, 39.96m },
                    { 2, 2, new DateTime(2024, 7, 5, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1329), 2, 23.98m },
                    { 3, 3, new DateTime(2024, 7, 5, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1331), 3, 77.94m },
                    { 4, 4, new DateTime(2024, 7, 5, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1333), 4, 79.96m },
                    { 5, 5, new DateTime(2024, 7, 5, 14, 27, 28, 136, DateTimeKind.Local).AddTicks(1335), 5, 149.95m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "ItemId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 4 },
                    { 2, 2, 2, 2 },
                    { 3, 3, 3, 6 },
                    { 4, 4, 4, 4 },
                    { 5, 5, 5, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 5);
        }
    }
}
