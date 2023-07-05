namespace CoreApp.Entities;
public class FileInformation
{
    public Guid Id { get; set; }
    public required string Extension { get; set; }
    public DateTime SavedDate { get; set; }
    public required string PathToFile { get; set; }
}