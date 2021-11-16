using HotelMgt.Core.interfaces;
using HotelMgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetPaymentAsync(string paymentId);
        Task<Payment> GetPaymentByTransactionReferenceAsync(string transactionRef);
        bool UpdatePayment(Payment model);
    }
}