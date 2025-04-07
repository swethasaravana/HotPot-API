﻿namespace HotPotAPI.Models.DTOs
{
    public class CreateDeliveryPartnerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string VehicleNumber { get; set; }
        

        public User? User { get; set; }
    }
}
