using CloudantDotNet.Models;
using System.Threading.Tasks;

namespace CloudantDotNet.Services
{
    public interface ICloudantService
    {
        Task<dynamic> CreateAsync(AddProductModel item);
        Task<dynamic> DeleteAsync(AddProductModel item);
        Task<dynamic> GetAllAsync();
        Task PopulateTestData();
        Task<string> UpdateAsync(AddProductModel item);
    }
}