namespace RabbitMQ.Publisher.API.Controller;

public interface IFileService
{
    public Task<string> SaveFileAsync(IFormFile file);
}