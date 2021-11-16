using CloudinaryDotNet.Actions;
using HotelMgt.Dtos.ImageDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface IImageService
    {
        Task<DelResResult> DeleteResourcesAsync(string publicId);
        Task<UploadResult> UploadImageAsync(IFormFile image);
    }
}
