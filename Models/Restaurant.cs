namespace HotPotAPI.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public ICollection<RestaurantManager> Managers { get; set; }

    }
}
