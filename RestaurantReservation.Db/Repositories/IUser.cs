using RestaurantReservation.DB.Models;
namespace RestaurantReservation.API.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }
}
