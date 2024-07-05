namespace RestaurantReservation.Db
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } //PK
        public int ItemId { get; set; } //FK (from MenuItem)
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; } // a Navigation property form the MenuItem to indicate the relationship
        public int OrderId { get; set; }// FK
        public Order Order { get; set; } // Navigatoin property for the 1:* with the orderitem class
    }
}
