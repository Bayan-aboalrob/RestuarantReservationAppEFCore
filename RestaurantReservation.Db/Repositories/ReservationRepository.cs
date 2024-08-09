using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContexts;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> UpdateReservationAsync(int id, Reservation reservation)
        {
            var existingReservation = await _context.Reservations.FindAsync(id);
            if (existingReservation == null)
            {
                return false;
            }

            existingReservation.CustomerId = reservation.CustomerId;
            existingReservation.TableId = reservation.TableId;
            existingReservation.ReservationDate = reservation.ReservationDate;
            existingReservation.PartySize = reservation.PartySize;

            _context.Reservations.Update(existingReservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId)
        {
            return await _context.Reservations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByReservationIdAsync(int reservationId)
        {
            return await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByReservationIdAsync(int reservationId)
        {
            return await _context.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId)
                .Include(oi => oi.MenuItem)
                .Select(oi => oi.MenuItem)
                .Distinct()
                .ToListAsync();
        }
    }
}
