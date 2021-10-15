using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.RoomTypeDtos
{
    public class AddRoomTypeDto
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Discount { get; set; }
        [Required]
        public string Thumbnail { get; set; }
    }
}
