using CoreApp.Entities.Enums;

namespace CoreApp.Entities;

public class Ticket
{
    public long Id { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public decimal  Price { get; set; }
    public ETicketStatus Status { get; set; }
    public required string OwnerFio { get; set; }
    public required string OwnerAdrress { get; set; }
    public int OwnerAge { get; set; }
    public DateTime FlightDateTime { get; set; }
    public DateTime BookingDateTime { get; set; }
    public DateTime? CancelledDateTime { get; set; }
}