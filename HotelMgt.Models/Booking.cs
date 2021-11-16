using System;

namespace HotelMgt.Models
{
    public class Booking : BaseModel
    {
        public string CustomerId { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public string BookingStatus { get; set; } = "Failed";
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public string RoomId { get; set; }
        public Room Room { get; set; }
    }
}
