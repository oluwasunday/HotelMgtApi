using HotelMgt.Dtos;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IPaymentService
    {
        Task<string> MakePaymentAsync(decimal amount, string email, string bookingId, string transactionRef);
        Task<Response<string>> VerifyPaymentAsync(string reference);
    }
}