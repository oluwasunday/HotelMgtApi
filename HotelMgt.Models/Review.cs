using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Models
{
    public class Review : BaseModel
    {
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
