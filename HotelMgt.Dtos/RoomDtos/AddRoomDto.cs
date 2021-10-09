using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.RoomDtos
{
    public class AddRoomDto
    {
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public string RoomTypeId { get; set; }
    }
}
