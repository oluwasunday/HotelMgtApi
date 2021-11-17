﻿using hotel_booking_models;
using HotelMgt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelMgt.Data
{
    public class HotelMgtDbContext : IdentityDbContext<AppUser>
    {

        public HotelMgtDbContext(DbContextOptions<HotelMgtDbContext> options) : base(options)
        {

        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Amenity> Amenities {  get; set; }
        public DbSet<Gallery> Galleries {  get; set; }
        public DbSet<ContactUs> ContactUs {  get; set; }

    }

}
