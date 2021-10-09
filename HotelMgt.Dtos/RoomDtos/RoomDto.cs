using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.RoomDtos
{
    public class RoomDto
    {
        public string Id { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public string RoomType { get; set; }
    }
}
