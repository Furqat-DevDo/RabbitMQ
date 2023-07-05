using Microsoft.AspNetCore.Http;

namespace CoreApp.Services.Abstraction;

public interface IFileService
{
    public Task<string?> SaveFileAsync(IFormFile file);
}