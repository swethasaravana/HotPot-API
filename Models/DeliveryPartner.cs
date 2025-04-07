namespace HotPotAPI.Models
{
    public class DeliveryPartner
    {
        public int PartnerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string VehicleNumber { get; set; }

        public User? User { get; set; }
    }
}
