using RestaurantReservation.Db.Models;
namespace RestaurantReservation.Api.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantIdAsync(int restaurantId);
        Task<MenuItem> GetMenuItemByIdAsync(int id);
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int id);
    }
}
