namespace CoreApp.Models;
public record FileModel
{
    public Guid Id { get; set; }
    public required string Extension { get; set; }
    public DateTime SavedDate { get; set; }
}