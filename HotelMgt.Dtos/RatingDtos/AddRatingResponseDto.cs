using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.RatingDtos
{
    public class AddRatingResponseDto
    {
        public string Id { get; set; }
        public int Ratings { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
