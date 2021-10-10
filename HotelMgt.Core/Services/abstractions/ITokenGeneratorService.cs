using HotelMgt.Models;
using System.Threading.Tasks;

namespace HotelMgt.Core.Services.abstractions
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateToken(AppUser model);
    }
}