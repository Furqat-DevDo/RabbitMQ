using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Publisher.API.Models;
using RabbitMQ.Publisher.API.Services;

namespace RabbitMQ.Publisher.API.Controller;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IPublisher _messageProducer;

    // In Memory DB
    private readonly List<Booking> _bookings = new ();

    public BookingController(ILogger<BookingController> logger,
    IPublisher messageProducer)
    {
        _logger = logger; 
        _messageProducer = messageProducer;
    }

    [HttpPost]

    public IActionResult Create([FromBody] Booking CreateModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _bookings.Add(CreateModel);

        _messageProducer.PublishMessage("booking",CreateModel);
        _logger.LogInformation($" Booking Created .");

        return Ok();
    }
}