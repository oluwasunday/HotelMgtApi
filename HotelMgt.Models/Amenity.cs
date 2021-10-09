using System;
using System.Collections.Generic;

namespace HotelMgt.Models
{ 
    public class Amenity : BaseModel
    {
        public string CustomerId  { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public decimal Discount { get; set; } 
        public Customer Customer { get; set; }
    }
}