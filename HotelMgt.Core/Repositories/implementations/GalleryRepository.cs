using hotel_booking_models;
using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;

namespace HotelMgt.Core.Repositories.implementations
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        private readonly HotelMgtDbContext _context;
        public GalleryRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateGallery(Gallery gallery)
        {
            _context.Galleries.Update(gallery);
        }
    }
}
