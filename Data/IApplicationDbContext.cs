using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.Models;

namespace Project.Data;

public interface IApplicationDbContext
{
    DbSet<Province> Provinces { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<Person> Persons { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
