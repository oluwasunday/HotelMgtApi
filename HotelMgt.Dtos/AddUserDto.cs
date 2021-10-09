using System;
using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Dtos
{
    public class AddUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

    }
}
