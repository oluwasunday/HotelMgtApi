using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Models
{
    public class RoomType : BaseModel
    {
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Thumbnail { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
