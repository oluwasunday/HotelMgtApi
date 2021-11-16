using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.BookingDtos
{
    public class AddBookingResponseDto
    {
        public string BookingReference { get; set; }
        public int RoomNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public string BookingStatus { get; set; }
        public string RoomType { get; set; }
        public bool PaymentStatus { get; set; }
        public decimal Price { get; set; }
        public string PaymentReference { get; set; }
        public string AuthorizationUrl { get; set; }
    }
}
