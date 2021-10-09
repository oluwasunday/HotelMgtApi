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
        public string Duration { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
    }
}
