using HotPotAPI.Models.DTOs;

namespace HotPotAPI.Interfaces
{
    public interface IAdminService
    {
        Task<CreateAdminResponse> CreateAdmin(CreateAdminRequest request);
    }
}
