using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Publisher.API.Services;

namespace RabbitMQ.Publisher.API.Controller;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IPublisher _messageProducer;

    public FilesController(IFileService fileService, IPublisher messageProducer)
    {
        _fileService = fileService;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var filePath = await _fileService.SaveFileAsync(file);
        _messageProducer.PublishMessage("files",filePath);
        return filePath is not null ? Ok(filePath) : BadRequest("Can not save file");
    }
}