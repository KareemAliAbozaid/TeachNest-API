using Microsoft.AspNetCore.Http;

namespace TechNest.Domain.Services
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
        void DeletImageAsync(string src);
    }
}
