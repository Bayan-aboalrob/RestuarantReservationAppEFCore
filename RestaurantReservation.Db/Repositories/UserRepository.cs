using RestaurantReservation.Db.Models;
using System.Linq;

namespace RestaurantReservation.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "testuser", Password = "password123" } 
        };

        User IUserRepository.GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
