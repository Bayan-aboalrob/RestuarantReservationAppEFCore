using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int RestaurantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public Restaurant Restaurant { get; set; } // I included this navigation property to represent the 1:* relationship btwn resturant and employee classes
        public List<Order> Orders { get; set; }
        public Employee()
        {
            Orders = new List<Order>();
        }

    }
}