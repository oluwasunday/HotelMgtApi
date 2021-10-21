using hotel_booking_models;
using HotelMgt.Core.interfaces;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        void UpdateGallery(Gallery gallery);
    }
}