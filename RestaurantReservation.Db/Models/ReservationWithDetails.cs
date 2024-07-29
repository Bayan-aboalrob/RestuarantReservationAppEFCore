public class ReservationWithDetails
{
    public int ReservationId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerEmail { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantAddress { get; set; }
    public string RestaurantPhoneNumber { get; set; }
}