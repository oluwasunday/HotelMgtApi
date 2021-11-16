using AutoMapper;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Dtos;
using HotelMgt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        private PayStackApi _payStack { get; set; }
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment env, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _env = env;
            _mapper = mapper;
            _payStack = new PayStackApi(_configuration["Payment:PaystackSecretKey"]);
        }

        public async Task<string> MakePaymentAsync(decimal amount, string email, string bookingId, string transactionRef)
        {
            Payment payment = new()
            {
                Amount = amount,
                MethodOfPayment = "Online payment",
                Status = false,
                TransactionReference = transactionRef,
                BookingId = bookingId
            };

            string baseUrl = _env.IsProduction() ? _configuration["HerokuUrl"] : _configuration["BaseUrl"];

            TransactionInitializeRequest trxRequest = new()
            {
                AmountInKobo = (int)amount * 100,
                Email = email,
                Reference = transactionRef,
                Currency = "NGN",
                CallbackUrl = $"{baseUrl}api/Booking/verify-booking"
            };


            TransactionInitializeResponse trxResponse = _payStack.Transactions.Initialize(trxRequest);
            if (trxResponse.Status)
            {
                await _unitOfWork.Payments.AddAsync(payment);
                return trxResponse.Data.AuthorizationUrl;
            }

            return trxResponse.Status.ToString();
        }

        public async Task<Response<string>> VerifyPaymentAsync(string reference)
        {
            //_logger.Information($"Attempt verify payemnt for {reference}");
            TransactionVerifyResponse response = _payStack.Transactions.Verify(reference);

            if (response.Data.Status == "success")
            {
                var payment = await _unitOfWork.Payments.GetPaymentByTransactionReferenceAsync(reference);
                payment.Status = true;

                var update = _unitOfWork.Payments.UpdatePayment(payment);
                await _unitOfWork.CompleteAsync();
                if (update)
                    return Response<string>.Success("success", $"Transaction reference - {payment.TransactionReference}");
            }

            //_logger.Information($"Attempt verify payment failed, gateway response {response.Data.GatewayResponse}");
            return Response<string>.Fail(response.Data.GatewayResponse);
        }
    }
}
