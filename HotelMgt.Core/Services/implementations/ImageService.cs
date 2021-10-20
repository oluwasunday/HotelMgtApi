using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Dtos.ImageDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.implementations
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        public ImageService(IConfiguration configuration, Cloudinary cloudinary)
        {
            _configuration = configuration;
            _cloudinary = cloudinary;
        }
        public async Task<UploadResult> UploadImageAsync(IFormFile image)
        {
            var pictureFormat = false;
            var listOfImageExtensions = _configuration.GetSection("PhotoSettings:Formats").Get<List<string>>();

            foreach (var item in listOfImageExtensions)
            {
                if (image.FileName.EndsWith(item))
                {
                    pictureFormat = true;
                    break;
                }
            }

            var pictureSize = Convert.ToInt64(_configuration["PhotoSettings:Size"]);
            if (image.Length > pictureSize)
                throw new ArgumentException("File size exceeded");

            if (pictureFormat == false)
                throw new ArgumentException("File format not supported");
            
            var uploadResult = new ImageUploadResult();

            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;
                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename, imageStream),
                    PublicId = "HotelMgt/" + filename,

                    Transformation = new Transformation()
                        .Crop("thumb")
                        .Gravity("face")
                        .Width(150)
                });
            }

            return uploadResult;
        }

        public async Task<DelResResult> DeleteResourcesAsync(string publicId)
        {
            DelResParams delParams = new DelResParams
            {
                PublicIds = new List<string> { publicId },
                All = true,
                KeepOriginal = false,
                Invalidate = true
            };

            DelResResult deletionResult = await _cloudinary.DeleteResourcesAsync(delParams);
            if (deletionResult.Error != null)
            {
                throw new ApplicationException($"" +
                    $"an error occured in method: " +
                    $"{nameof(DeleteResourcesAsync)}" +
                    $" class: {nameof(ImageService)}");
            }

            return deletionResult;
        }
    }
}
