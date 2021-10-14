using System;

namespace HotelMgt.Dtos.ReviewDtos
{
    public class AddReviewResponseDto
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
