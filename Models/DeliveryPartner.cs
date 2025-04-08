using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotPotAPI.Models
{
    public class DeliveryPartner
    {
        [Key]
        public int PartnerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string VehicleNumber { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }  // Foreign Key

        public User? User { get; set; }       // Navigation Property
    }
}
