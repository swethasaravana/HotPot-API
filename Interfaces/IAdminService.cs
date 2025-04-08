using HotPotAPI.Models;
using HotPotAPI.Models.DTOs;

namespace HotPotAPI.Interfaces
{
    public interface IAdminService
    {
        Task<CreateAdminResponse> CreateAdmin(CreateAdminRequest request);
        Task<Restaurant> AddRestaurant(CreateRestaurantDTO dto);
        Task<Restaurant> UpdateRestaurant(int id, CreateRestaurantDTO dto);
        Task<bool> DeleteRestaurant(int id);
        Task<List<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetRestaurantById(int id);
    }
}
