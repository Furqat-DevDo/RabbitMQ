namespace CoreApplication.Entities;

public class Ticket
{
    public int Id { get; set;}
    public required string Status { get; set;}
    public required string From { get; set; }
    public required string To { get; set; }
    public decimal  Price { get; set; }
}