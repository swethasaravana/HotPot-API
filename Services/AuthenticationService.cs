using HotPotAPI.Interfaces;
using HotPotAPI.Models;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Repositories;
using System.Security.Cryptography;

namespace HotPotAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IRepository<string, User> userRpository,
                                     IRepository<int, Customer> customerRepository, ITokenService tokenService)
        {
            _userRepository = userRpository;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Login(UserLoginRequest loginRequest)
        {
            var user = await _userRepository.GetById(loginRequest.Username);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            HMACSHA512 hmac = new HMACSHA512(user.HashKey);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginRequest.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new UnauthorizedAccessException("Invalid password");
            }

            string name = "";
            int id = 0;

            if (user.Role == "Customer")
            {
                var client = (await _customerRepository.GetAll()).FirstOrDefault(c => c.Email == loginRequest.Username);
                if (client == null)
                    throw new UnauthorizedAccessException("Customer not found");
                name = client.Name;
                id = client.Id;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid role");
            }

            var token = await _tokenService.GenerateToken(id, name, user.Role);
            return new LoginResponse { Id = id, Name = name, Role = user.Role, Token = token };
        }
    }
}
