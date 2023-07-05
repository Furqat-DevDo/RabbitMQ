using CoreApp.Models;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Services.Abstraction;

public interface IFileService
{
    public Task<FileModel?> SaveFileAsync(IFormFile file);
}