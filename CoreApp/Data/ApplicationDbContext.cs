using CoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options): base (options)
    {}

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<FileInformation> Files => Set<FileInformation>();
}