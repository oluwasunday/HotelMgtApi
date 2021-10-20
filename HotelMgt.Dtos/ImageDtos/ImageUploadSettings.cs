using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Dtos.ImageDtos
{
    public class ImageUploadSettings
    {
        public string CloudAccountName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
