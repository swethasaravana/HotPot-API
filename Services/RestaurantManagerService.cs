using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace HotPotAPI.Services
{
    public class RestaurantManagerService : IRestaurantManagerService
    {
        public Task<CreateResturantManagerResponse> AddRestaurantManager(CreateRestaurantManagerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
