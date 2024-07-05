using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task AddReservationAsync(Reservation newReservation)
        {
            var existingCustomer = await _context.Customers.FindAsync(newReservation.CustomerId);
            if (existingCustomer == null)
            {
                throw new InvalidOperationException("The specified customer does not exist.");
            }

            var existingRestaurant = await _context.Restaurants.FindAsync(newReservation.RestaurantId);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("The specified restaurant does not exist.");
            }

            var existingTable = await _context.Tables.FindAsync(newReservation.TableId);
            if (existingTable == null)
            {
                throw new InvalidOperationException("The specified table does not exist.");
            }

            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();
            Console.WriteLine("Created Reservation");
        }

        public async Task UpdateReservationAsync(int reservationId, DateTime newReservationDate, int newPartySize, int newCustomerId, int newRestaurantId, int newTableId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                var existingCustomer = await _context.Customers.FindAsync(newCustomerId);
                if (existingCustomer == null)
                {
                    throw new InvalidOperationException("The specified customer does not exist.");
                }

                var existingRestaurant = await _context.Restaurants.FindAsync(newRestaurantId);
                if (existingRestaurant == null)
                {
                    throw new InvalidOperationException("The specified restaurant does not exist.");
                }

                var existingTable = await _context.Tables.FindAsync(newTableId);
                if (existingTable == null)
                {
                    throw new InvalidOperationException("The specified table does not exist.");
                }

                reservation.ReservationDate = newReservationDate;
                reservation.PartySize = newPartySize;
                reservation.CustomerId = newCustomerId;
                reservation.RestaurantId = newRestaurantId;
                reservation.TableId = newTableId;

                await _context.SaveChangesAsync();
                Console.WriteLine("Updated Reservation");
            }
            else
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation != null)
            {
                foreach (var order in reservation.Orders)
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                    _context.Orders.Remove(order);
                }

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                Console.WriteLine("Deleted Reservation");
            }
            else
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
        }

        public async Task<List<ReservationWithDetails>> ListReservationsWithDetailsAsync()
        {
            return await _context.ReservationsWithDetails.ToListAsync();
        }
    }
}
