using HotPotAPI.Models.DTOs;

namespace HotPotAPI.Interfaces
{
    public interface IRestaurantManagerService
    {
        Task<CreateResturantManagerResponse> AddRestaurantManager(CreateRestaurantManagerRequest request);

    }
}
