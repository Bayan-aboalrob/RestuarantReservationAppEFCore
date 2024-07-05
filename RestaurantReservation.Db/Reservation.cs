namespace RestaurantReservation.Db
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int RestaurantId { get; set; }// Fk
        public Restaurant Restaurant { get; set; } // navigation property for the 1:* with the resturant
        public DateTime ReservationDate { get; set; }
        public int PartySize { get; set; }
        public List<Order> Orders { get; set; }
        public int CustomerId { get; set; }//FK
        public Customer Customer { get; set; } // Navigation property for 1:* with the customer table
        public int TableId { get; set; }// FK
        public Table Table { get; set; } // Naviagation property for the 1:* with the Table class
        public Reservation()
        {
            Orders = new List<Order>();
        }
    }
}
