namespace HotelMgt.Models
{
    public class Rating : BaseModel
    {
        public int Ratings { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
