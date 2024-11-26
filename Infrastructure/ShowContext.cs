using Domain.Shows;
using Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure;

/// <summary>
/// Show database context for Entity Framework
/// </summary>
public class ShowContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Show> Shows => Set<Show>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowEntityTypeConfiguration).Assembly);
    }
}

/// <summary>
/// Scaffold stuff.
/// </summary>
public class ApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShowContext>
{
    public ShowContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ShowContext> optionsBuilder = new DbContextOptionsBuilder<ShowContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=testdb;User Id=sa;Password=ha-ha-grappig-hoor;Trusted_Connection=True;");

        return new ShowContext(optionsBuilder.Options);
    }
}