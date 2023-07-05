using CoreApp.Data;
using CoreApp.Entities.Enums;
using CoreApp.Mappers;
using CoreApp.Models;
using CoreApp.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreApp.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BookingService> _logger;
    private readonly ITicketMapper _ticketMapper;
    private readonly IPublisher _messageProducer;

    public BookingService(ApplicationDbContext dbContext, 
        ILogger<BookingService> logger, 
        ITicketMapper ticketMapper, IPublisher messageProducer)
    {
        _dbContext = dbContext;
        _logger = logger;
        _ticketMapper = ticketMapper;
        _messageProducer = messageProducer;
    }
    public async  Task<BookingModel> CreateBookingAsync(BookingCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var ticket = _ticketMapper.ToEntity(createModel);
        
        await _dbContext.Tickets.AddAsync(ticket,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"New booking created : Id: {ticket.Id} From : {ticket.From}  To : {ticket.To}");
        _messageProducer.PublishMessage("booking",ticket);
        
        return _ticketMapper.ToModel(ticket);
    }

    public async Task<BookingModel?> GetBookingByIdAsync(long bookingId, CancellationToken cancellationToken = default)
    {
        var booking = await  _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == bookingId,cancellationToken);
        if (booking is null) return null;
        return _ticketMapper.ToModel(booking);
    }

    public async  Task<bool> CancellBookingAsync(long bookingId, CancellationToken cancellationToken = default)
    {
        var booking = await  _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == bookingId,cancellationToken);
        if (booking is null) return false;

        booking.Status = ETicketStatus.Cancelled;
        booking.CancelledDateTime = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}