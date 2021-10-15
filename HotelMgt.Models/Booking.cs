using System;
using System.Collections.Generic;

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
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
    }
}
