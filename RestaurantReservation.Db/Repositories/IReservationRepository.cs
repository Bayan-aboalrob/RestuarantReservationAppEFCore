using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task<bool> UpdateReservationAsync(int id, Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersByReservationIdAsync(int reservationId);
        Task<IEnumerable<MenuItem>> GetMenuItemsByReservationIdAsync(int reservationId);
    }
}
