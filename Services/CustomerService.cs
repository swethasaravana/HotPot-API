using HotPotAPI.Interfaces;
using HotPotAPI.Models;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace HotPotAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Restaurant> _restaurantRepository;


        public CustomerService(IRepository<string, User> userRepository, IRepository<int, Customer> customerRepository, IRepository<int, Restaurant> restaurantRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<CreateCustomerResponse> AddCustomer(CreateCustomerRequest request)
        {
            HMACSHA512 hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            var user = MapCustomerToUser(request, passwordHash, hmac.Key);

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");
            var customer = MapCustomer(request);
            customer.User = user; // set navigation property

            var customerResult = await _customerRepository.Add(customer);
            if (customerResult == null)
                throw new Exception("Failed to create customer");

            return new CreateCustomerResponse { Id = customerResult.Id };
        }

        private Customer MapCustomer(CreateCustomerRequest request)
        {
            return new Customer
            {
                Name = request.Name,
                Gender = request.Gender,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };
        }

        private User MapCustomerToUser(CreateCustomerRequest request, byte[] passwordHash, byte[] key)
        {
            return new User
            {
                Username = request.Email,
                Password = passwordHash,
                HashKey = key,
                Role = "Customer"
            };
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAll();
            return restaurants.ToList();
        }




    }

}
