using CoreApp.Data;
using CoreApp.Models;
using CoreApp.Services.Abstraction;
using Microsoft.Extensions.Logging;

namespace CoreApp.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BookingService> _logger;

    public BookingService(ApplicationDbContext dbContext, ILogger<BookingService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public Task<BookingModel> CreateBookingAsync(BookingCreateModel createModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BookingModel?> GetBookingByIdAsync(long bookingId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancellBookingAsync(long bookingId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}