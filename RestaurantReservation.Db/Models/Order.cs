using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RestaurantReservation.Db.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }//FK
        public Employee Employee { get; set; } // navigation propert for 1:* with employee table
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }

        public List<OrderItem> OrderItems;
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
