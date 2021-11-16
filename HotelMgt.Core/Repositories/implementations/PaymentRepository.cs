using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Data;
using HotelMgt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Repositories.implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly HotelMgtDbContext _context;
        public PaymentRepository(HotelMgtDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentAsync(string paymentId)
        {
            var payment = await _context.Payments
                .Where(x => x.BookingId == paymentId)
                .Include(x => x.Booking)
                    .ThenInclude(y => y.Customer)
                .FirstOrDefaultAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            var payments = await _context.Payments
                .Where(x => x.Status == true)
                .Include(y => y.Booking)
                    .ThenInclude(y => y.Customer)
                    .ToListAsync();
            return payments;
        }

        public async Task<Payment> GetPaymentByTransactionReferenceAsync(string transactionRef)
        {
            var payments = await _context.Payments
                .Where(x => x.TransactionReference == transactionRef)
                .Include(y => y.Booking)
                    .ThenInclude(y => y.Customer)
                    .FirstOrDefaultAsync();
            return payments;
        }

        public bool UpdatePayment(Payment model)
        {
            var payment = _context.Payments.Update(model);
            return true;
        }
    }
}
