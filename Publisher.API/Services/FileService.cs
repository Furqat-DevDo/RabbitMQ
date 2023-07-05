using RabbitMQ.Publisher.API.Controller;

namespace RabbitMQ.Publisher.API.Services;

public class FileService : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile file)
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

            using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(fileStream);

            Console.WriteLine("File saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving the file: " + ex.Message);
        }

        return filePath;
    }
}