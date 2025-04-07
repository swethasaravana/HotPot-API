namespace HotPotAPI.Models
{
    public class RestaurantManager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Phone { get; set; }

        // Foreign key reference
        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public User? User { get; set; }

    }
}
