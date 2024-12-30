namespace ParkingService.Models
{
    public class ParkingSpot
    {
        public int id { get; set; }
        public string location { get; set; }
        public bool is_available { get; set; }
        public string type { get; set; } 
        public decimal price_per_hour { get; set; }
    }
}
