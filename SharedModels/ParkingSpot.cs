namespace ParkingService.Models
{
    public class ParkingSpot
    {
        public int id { get; set; }
        public string location { get; set; }
        public bool isavailable { get; set; }
        public int totalspots { get; set; }
        public bool availablespots { get; set; }
        public string type { get; set; }
        public decimal priceperhour { get; set; }
    }
}
