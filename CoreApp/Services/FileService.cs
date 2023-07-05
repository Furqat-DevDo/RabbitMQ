using CoreApp.Data;
using CoreApp.Entities;
using CoreApp.Mappers;
using CoreApp.Models;
using CoreApp.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoreApp.Services;

public class FileService : IFileService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<FileService> _logger;
    private readonly IFileMapper _fileMapper;
    private readonly IPublisher _messageProducer;
    public FileService(ApplicationDbContext dbContext, 
        ILogger<FileService> logger, 
        IFileMapper fileMapper, 
        IPublisher messageProducer)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileMapper = fileMapper;
        _messageProducer = messageProducer;
    }
    
    public async Task<FileModel?> SaveFileAsync(IFormFile file)
    {
        if(file is null) throw new ArgumentNullException(nameof(file));

        var path = await SaveFileToFolderAsync(file);

        var entity = new FileInformation
        {
            PathToFile = path,
            Id = Guid.NewGuid(),
            Extension = file.ContentType,
            SavedDate = DateTime.UtcNow
        };
        await _dbContext.Files.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        _messageProducer.PublishMessage("files",entity);
        _logger.LogInformation($"New File Saved and message published to queue {entity.Id}");
        
        return _fileMapper.ToModel(entity);
    }

    private async Task<string> SaveFileToFolderAsync(IFormFile file)
    {
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