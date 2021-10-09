using System;
using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Dtos
{
    public class AddUserResponseDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

    }
}
