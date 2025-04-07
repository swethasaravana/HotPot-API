using HotPotAPI.Models.DTOs;

namespace HotPotAPI.Interfaces
{
    public interface IDeliveryPartnerService
    {
        Task<CreateDeliveryPartnerResponse> AddDeliveryPartner(CreateDeliveryPartnerRequest request);
    }
}
