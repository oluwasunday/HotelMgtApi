using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.PaymentDtos
{
    public class MakePaymentDto
    {
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public string BookingId { get; set; }
        public string TransactionRef { get; set; }
    }
}
