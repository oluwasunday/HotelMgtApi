using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.ContactUsDtos
{
    public class AddContactUsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
