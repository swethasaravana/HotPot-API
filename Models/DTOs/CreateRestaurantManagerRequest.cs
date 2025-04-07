﻿namespace HotPotAPI.Models.DTOs
{
    public class CreateRestaurantManagerRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? RestaurantId { get; set; }
    }
}
