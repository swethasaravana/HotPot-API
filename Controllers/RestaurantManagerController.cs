using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using HotPotAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotPotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantManagerController : ControllerBase
    {
        private readonly IRestaurantManagerService _service;

        public RestaurantManagerController(IRestaurantManagerService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateResturantManagerResponse>> AddRestaurantManager(CreateRestaurantManagerRequest request)
        {
            try
            {
                var result = await _service.AddRestaurantManager(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
