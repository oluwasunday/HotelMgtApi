using System;
using System.Collections.Generic;

namespace HotelMgt.Models
{ 
    public class Amenity : BaseModel
    {
        public string Name { get; set; }
        public string RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
    }
}