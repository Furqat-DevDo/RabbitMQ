using CoreApp.Models;
using CoreApp.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IPublisher _messageProducer;
    private readonly IBookingService _bookingService;

    public BookingController(ILogger<BookingController> logger,
    IPublisher messageProducer, 
    IBookingService bookingService)
    {
        _logger = logger; 
        _messageProducer = messageProducer;
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel createModel,CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdResult = await _bookingService.CreateBookingAsync(createModel,cancellationToken);

        _messageProducer.PublishMessage("booking",createdResult);
        _logger.LogInformation(" Booking created event raised , message published to the queue.");
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