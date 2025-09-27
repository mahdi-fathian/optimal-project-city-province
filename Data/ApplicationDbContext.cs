using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Province> Provinces { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.ProvinceId }).IsUnique();
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
