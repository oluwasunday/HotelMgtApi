using HotelMgt.Models;

namespace hotel_booking_models
{
    public class Gallery : BaseModel
    {
        public string RoomId { get; set; }
        public string ImageUrl { get; set; } 
        public bool IsFeature { get; set; }
        public Room Room { get; set; }
    }
}
