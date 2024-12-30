namespace ParkingService.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int parking_spot_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public double total_price { get; set; }
    }
}
