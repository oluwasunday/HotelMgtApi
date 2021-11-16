﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Models
{
    public class Payment
    {
        [Key]
        public string BookingId { get; set; }
        public string TransactionReference { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public string MethodOfPayment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Booking Booking { get; set; }
    }
}
