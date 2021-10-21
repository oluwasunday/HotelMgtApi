using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.GalleryDtos
{
    public class AddGalleryDto
    {
        [Required]
        public string RoomId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public bool IsFeature { get; set; }
    }
}
