namespace RestaurantReservation.Db.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int RestaurantId { get; set; } // FK
        public Restaurant Restaurant { get; set; } //Navigation property for the 1:* with the resturant table
        public Table()
        {
            Reservations = new List<Reservation>();
        }
    }
}
