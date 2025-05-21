using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; } = null!;
    public DbSet<EventAddressEntity> EventAddresses { get; set; } = null!;
    public DbSet<PackageEntity> Packages { get; set; } = null!;
    public DbSet<EventPackageEntity> EventsPackages { get; set; } = null!;

}
