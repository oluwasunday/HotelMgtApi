using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.BookingDtos
{
    public class VerifyBookingDto
    {
        [Required]
        public string TrxRef { get; set; }
        public string TrxId { get; set; }
    }
}
