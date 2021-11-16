using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Models
{
    public class Rating : BaseModel
    {
        public int Ratings { get; set; }
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
