using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.GalleryDtos
{
    public class AddImageDto
    {
        [Required]
        public IFormFile ImageUrl { get; set; }
        [Required]
        public bool IsFeature { get; set; } = false;
    }
}
