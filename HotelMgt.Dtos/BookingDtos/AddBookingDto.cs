using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.BookingDtos
{
    public class AddBookingDto
    {
        public string CustomerId { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
