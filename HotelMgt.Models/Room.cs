﻿namespace HotelMgt.Models
{
    public class Room : BaseModel
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public RoomType Roomtype { get; set; }
    }
}
