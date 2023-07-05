using CoreApp.Entities;
using Microsoft.EntityFrameworkCore;
using FileInfo = CoreApp.Entities.FileInfo;

namespace CoreApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options): base (options)
    {}

    public DbSet<Ticket> Tickets { get; set; }  
    public DbSet<FileInfo> Files { get; set; }
}