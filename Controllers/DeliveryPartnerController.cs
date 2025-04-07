using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotPotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPartnerController : ControllerBase
    {
        private readonly IDeliveryPartnerService _deliveryPartnerService;

        public DeliveryPartnerController(IDeliveryPartnerService deliveryPartnerService)
        {
            _deliveryPartnerService = deliveryPartnerService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateDeliveryPartnerResponse>> Register(CreateDeliveryPartnerRequest request)
        {
            try
            {
                var result = await _deliveryPartnerService.AddDeliveryPartner(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
