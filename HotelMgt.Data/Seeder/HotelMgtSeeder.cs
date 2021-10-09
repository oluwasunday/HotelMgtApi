using HotelMgt.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Data.Seeder
{
    public class HotelMgtSeeder
    {
        public static async Task SeedData(HotelMgtDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Manager", "Customer" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
                var users = new List<AppUser> {
                   new AppUser
                   {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Hotel",
                        LastName = "Admin",
                        UserName = "hotelmgtadmin",
                        Email = "info@hotelmgt.com",
                        PhoneNumber = "09043546576",
                        Gender = "Male",
                        Age = 34,
                        IsActive = true,
                        PublicId = null,
                        Avatar = "http://placehold.it/32x32",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                   },
                   new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Hotels",
                        LastName = "Manager",
                        UserName = "hotelmgtmanager",
                        Email = "info@managerhotelmgt.com",
                        PhoneNumber = "09043546522",
                        Gender = "Female",
                        Age = 37,
                        IsActive = true,
                        PublicId = null,
                        Avatar = "http://placehold.it/32x32",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Password@123");
                    if (user == users[0])
                        await userManager.AddToRoleAsync(user, "Admin");
                    else
                        await userManager.AddToRoleAsync(user, "Manager");
                }


                var path = File.ReadAllText(baseDir + @"/jsons/users.json");

                var hotelMgtUsers = JsonConvert.DeserializeObject<List<AppUser>>(path);
                foreach (var appuser in hotelMgtUsers)
                {
                    await userManager.CreateAsync(appuser, "Password@123");
                    await userManager.AddToRoleAsync(appuser, "Customer");
                }
            }



            // Bookings and Payment
            if (!dbContext.Bookings.Any())
            {
                var path = File.ReadAllText(baseDir + @"/jsons/bookings.json");

                var bookings = JsonConvert.DeserializeObject<List<Booking>>(path);
                await dbContext.Bookings.AddRangeAsync(bookings);
            }


            // Ratings
            if (!dbContext.Ratings.Any())
            {
                var path = File.ReadAllText(baseDir + @"/jsons/ratings.json");

                var ratings = JsonConvert.DeserializeObject<List<Rating>>(path);
                await dbContext.Ratings.AddRangeAsync(ratings);
            }

            // Reviews
            if (!dbContext.Reviews.Any())
            {
                var path = File.ReadAllText(baseDir + @"/jsons/reviews.json");

                var reviews = JsonConvert.DeserializeObject<List<Review>>(path);
                await dbContext.Reviews.AddRangeAsync(reviews);
            }

            // Roomtypes and rooms
            if (!dbContext.RoomTypes.Any())
            {
                var path = File.ReadAllText(baseDir + @"/jsons/roomtypes.json");

                var roomTypes = JsonConvert.DeserializeObject<List<RoomType>>(path);
                await dbContext.RoomTypes.AddRangeAsync(roomTypes);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
