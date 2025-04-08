using HotPotAPI.Interfaces;
using HotPotAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotPotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateAdminResponse>> CreateAdmin(CreateAdminRequest request)
        {
            try
            {
                var result = await _adminService.CreateAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("restaurants")]
        public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDTO dto)
        {
            try
            {
                var added = await _adminService.AddRestaurant(dto);
                return Ok(added);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }

        // Update Restaurant
        [HttpPut("restaurants/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] CreateRestaurantDTO dto)
        {
            try
            {
                var updated = await _adminService.UpdateRestaurant(id, dto);
                if (updated == null)
                    return NotFound("Restaurant not found.");

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Delete Restaurant
        [HttpDelete("restaurants/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                var result = await _adminService.DeleteRestaurant(id);
                if (result)
                    return NoContent();
                return NotFound("Restaurant not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Get All Restaurants
        [HttpGet("restaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            try
            {
                var restaurants = await _adminService.GetAllRestaurants();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // Get Restaurant by Id
        [HttpGet("restaurants/{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            try
            {
                var restaurant = await _adminService.GetRestaurantById(id);
                if (restaurant == null)
                    return NotFound("Restaurant not found.");
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
