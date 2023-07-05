using CoreApp.Models;
using CoreApp.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel createModel,CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdResult = await _bookingService.CreateBookingAsync(createModel,cancellationToken);
        return Ok(createdResult);
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetBooking([FromRoute] uint id,CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var searchResult = await _bookingService.GetBookingByIdAsync(id,cancellationToken);
        return searchResult is null ? NotFound(searchResult) : Ok(searchResult);
    }

    [HttpPut("id")]
    public async Task<IActionResult> CancellBooking([FromRoute] uint id, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cancellactionResult = await _bookingService.CancellBookingAsync(id,cancellationToken);
        return cancellactionResult ? NotFound(cancellactionResult) : Ok(cancellactionResult);
    }
}