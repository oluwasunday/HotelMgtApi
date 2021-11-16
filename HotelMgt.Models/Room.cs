using hotel_booking_models;
using System.Collections.Generic;

namespace HotelMgt.Models
{
    public class Room : BaseModel
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public RoomType Roomtype { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
    }
}
