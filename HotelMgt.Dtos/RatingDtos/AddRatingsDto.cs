using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Dtos.RatingDtos
{
    public class AddRatingsDto
    {
        public int Ratings { get; set; }
        public string Comment { get; set; }
    }
}
