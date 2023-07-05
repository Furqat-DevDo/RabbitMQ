using CoreApp.Models;

namespace CoreApp.Services.Abstraction;

public interface IBookingService
{
    public Task<BookingModel> CreateBookingAsync(BookingCreateModel createModel,CancellationToken cancellationToken = default);
    public Task<BookingModel?> GetBookingByIdAsync(long bookingId,CancellationToken cancellationToken = default);
    public Task<bool> CancellBookingAsync(long bookingId,CancellationToken cancellationToken = default);
}