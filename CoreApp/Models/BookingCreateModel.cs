namespace CoreApp.Models;

public record BookingCreateModel
{
    public required OwnerInfo OwnerInfo { get; set; }
    public required string Status { get; set;}
    public required string From { get; set; }
    public required string To { get; set; }
    public decimal  Price { get; set; }
    public DateTime FlightDateTime { get; set; }
    public DateTime BookingDateTime { get; set; }
    public DateTime? CancelledDateTime { get; set; }
}