using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.AuthenticationDto
{
    public class RegisterResponseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PublicId { get; set; }
        public string Avatar { get; set; }
    }
}
