using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.CustomerDtos
{
    public class AddCustomerResponseDto
    {
        public string AppUserId { get; set; }
        public string CreditCard { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}
