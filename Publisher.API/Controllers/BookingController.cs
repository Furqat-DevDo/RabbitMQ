using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Publisher.API.Models;
using RabbitMQ.Publisher.API.Services;

namespace RabbitMQ.Publisher.API.Controller;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IMessageProducer _messageProducer;

    // In Memory DB
    private readonly List<Booking> _bookings = new List<Booking>();

    public BookingController(ILogger<BookingController> logger,IMessageProducer messageProducer)
    {
        _logger = logger; 
        _messageProducer = messageProducer;
    }

    [HttpPost]

    public IActionResult Create([FromBody] Booking CreateModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _bookings.Add(CreateModel);

        _messageProducer.SendMessage<Booking>(CreateModel);

        _logger.LogInformation($" Booking Created .");

        return Ok();
    }
}