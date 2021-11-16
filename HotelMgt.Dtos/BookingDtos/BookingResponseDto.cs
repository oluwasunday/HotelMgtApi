using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.BookingDtos
{
    public class BookingResponseDto
    {
        public string Id { get; set; }
        public int RoomNo { get; set; }
        public string RoomType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string BookingReference { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
