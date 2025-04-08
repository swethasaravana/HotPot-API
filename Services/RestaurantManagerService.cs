using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace HotPotAPI.Services
{
    public class RestaurantManagerService : IRestaurantManagerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, RestaurantManager> _managerRepository;
        private readonly IRepository<int, Restaurant> _restaurantRepository;

        public RestaurantManagerService(
            IRepository<string, User> userRepository,
            IRepository<int, RestaurantManager> managerRepository,
            IRepository<int, Restaurant> restaurantRepository)
        {
            _userRepository = userRepository;
            _managerRepository = managerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<CreateResturantManagerResponse> AddRestaurantManager(CreateRestaurantManagerRequest request)
        {
            HMACSHA512 hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            var user = MapManagerToUser(request, passwordHash, hmac.Key);

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var manager = MapManager(request);
            manager.User = user; // set navigation property

            var managerResult = await _managerRepository.Add(manager);
            if (managerResult == null)
                throw new Exception("Failed to create manager");

            var restaurant = await _restaurantRepository.GetById(managerResult.RestaurantId);

            return new CreateResturantManagerResponse
            {
                Id = managerResult.ManagerId,
                FullName = managerResult.Username,
                Email = managerResult.Email,
                Phone = managerResult.PhoneNumber,
                RestaurantName = restaurant?.RestaurantName
            };
        }

        private RestaurantManager MapManager(CreateRestaurantManagerRequest request)
        {
            return new RestaurantManager
            {
                Username = request.FullName,
                Email = request.Email,
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)),
                PhoneNumber = request.PhoneNumber,
                RestaurantId = request.RestaurantId ?? 0
            };
        }

        private User MapManagerToUser(CreateRestaurantManagerRequest request, byte[] passwordHash, byte[] key)
        {
            return new User
            {
                Username = request.Email,
                Password = passwordHash,
                HashKey = key,
                Role = "RestaurantManager"
            };
        }
    }
}
