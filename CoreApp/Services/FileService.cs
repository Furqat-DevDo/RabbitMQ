using CoreApp.Data;
using CoreApp.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoreApp.Services;

public class FileService : IFileService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<FileService> _logger;
    public FileService(ApplicationDbContext dbContext, ILogger<FileService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<string?> SaveFileAsync(IFormFile file)
    {
        if(file is null) throw new ArgumentNullException(nameof(file));

        var folderPath = ".\\Files";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, file.FileName);

        try
        {
            await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(fileStream);

            _logger.LogInformation("File saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error saving the file: " + ex.Message);
        }

        return filePath;
    }
}