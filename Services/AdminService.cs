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

        public AdminService(IRepository<string, User> userRepository, IRepository<int, Admin> adminRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
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
    }
}
