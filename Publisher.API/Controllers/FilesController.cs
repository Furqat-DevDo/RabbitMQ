using CoreApp.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var filePath = await _fileService.SaveFileAsync(file);
        return filePath is not null ? Ok(filePath) : BadRequest("Can not save file");
    }
}
