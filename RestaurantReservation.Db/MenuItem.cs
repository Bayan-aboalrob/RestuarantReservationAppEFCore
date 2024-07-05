namespace RestaurantReservation.Db
{
    public class MenuItem
    {
        public int ItemId { get; set; }//PK
        public int RestaurantId { get; set; } //FK 
        public Restaurant Restaurant { get; set; } //Navigation property (for the 1:*) relation wiht the resturant
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; } // I include this Navigatoin property from the order item table for (1:*) relationship with orderitem class
        public MenuItem()
        {
            OrderItems = new List<OrderItem>();
        }



    }
}
