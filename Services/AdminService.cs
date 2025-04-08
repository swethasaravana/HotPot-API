using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace HotPotAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<int, Restaurant> _restaurantRepository;

        public AdminService(IRepository<string, User> userRepository, IRepository<int, Admin> adminRepository, IRepository<int, Restaurant> restaurantRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<CreateAdminResponse> CreateAdmin(CreateAdminRequest request)
        {
            HMACSHA512 hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            var user = MapAdminToUser(request, passwordHash, hmac.Key);

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var admin = MapAdmin(request);
            admin.User = user;

            var adminResult = await _adminRepository.Add(admin);
            if (adminResult == null)
                throw new Exception("Failed to create administrator");

            return new CreateAdminResponse { Id = adminResult.Id };
        }

        private Admin MapAdmin(CreateAdminRequest request)
        {
            return new Admin
            {
                Name = request.Name,
                Email = request.Email
            };
        }

        private User MapAdminToUser(CreateAdminRequest request, byte[] passwordHash, byte[] key)
        {
            return new User
            {
                Username = request.Email,
                Password = passwordHash,
                HashKey = key,
                Role = "Admin"
            };
        }
        public async Task<Restaurant> AddRestaurant(CreateRestaurantDTO dto)
        {
            var restaurant = new Restaurant
            {
                RestaurantName = dto.Name,
                Location = dto.Address,
                ContactNumber = dto.Contact,
                Email = dto.Email,
                Restaurantlogo = dto.Restaurantlogo
            };
            return await _restaurantRepository.Add(restaurant);
        }

        public async Task<Restaurant> UpdateRestaurant(int id, CreateRestaurantDTO dto)
        {
            var existing = await _restaurantRepository.GetById(id);
            if (existing == null)
            {
                throw new Exception("Restaurant not found");
            }

            existing.RestaurantName = dto.Name;
            existing.Location = dto.Address;
            existing.ContactNumber = dto.Contact;
            existing.Email = dto.Email;
            existing.Restaurantlogo = dto.Restaurantlogo;

            return await _restaurantRepository.Update(existing.RestaurantId, existing);
        }
        public async Task<bool> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            if (restaurant == null)
                return false;

            var deleted = await _restaurantRepository.Delete(id);
            return deleted != null;
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAll();
            return restaurants.ToList();
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _restaurantRepository.GetById(id);
        }
    }
}

