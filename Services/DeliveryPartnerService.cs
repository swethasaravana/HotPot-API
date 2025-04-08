using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace HotPotAPI.Services
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, DeliveryPartner> _partnerRepository;

        public DeliveryPartnerService(
            IRepository<string, User> userRepository,
            IRepository<int, DeliveryPartner> partnerRepository)
        {
            _userRepository = userRepository;
            _partnerRepository = partnerRepository;
        }

        public async Task<CreateDeliveryPartnerResponse> AddDeliveryPartner(CreateDeliveryPartnerRequest request)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            var user = MapToUser(request, passwordHash, hmac.Key);
            var userResult = await _userRepository.Add(user);

            if (userResult == null)
                throw new Exception("Failed to create user");

            var partner = MapToDeliveryPartner(request);
            partner.Username = user.Username; ;

            var partnerResult = await _partnerRepository.Add(partner);
            if (partnerResult == null)
                throw new Exception("Failed to create delivery partner");

            return new CreateDeliveryPartnerResponse { PartnerId = partnerResult.PartnerId };
        }

        private DeliveryPartner MapToDeliveryPartner(CreateDeliveryPartnerRequest request)
        {
            return new DeliveryPartner
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                VehicleNumber = request.VehicleNumber
            };
        }

        private User MapToUser(CreateDeliveryPartnerRequest request, byte[] hash, byte[] key)
        {
            return new User
            {
                Username = request.Email,
                Password = hash,
                HashKey = key,
                Role = "DeliveryPartner"
            };
        }
    }
}
