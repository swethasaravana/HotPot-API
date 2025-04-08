using HotPotAPI.Models;
using HotPotAPI.Models.DTOs;

namespace HotPotAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponse> AddCustomer(CreateCustomerRequest request);
        Task<List<Restaurant>> GetAllRestaurants();


    }
}
