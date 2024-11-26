using Domain.Shows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

/// <summary>
/// Show Repository
/// </summary>
/// <param name="databaseContext">Database context</param>
public sealed class ShowRepository(ShowContext databaseContext) : IShowRepository
{
    public ValueTask<EntityEntry<Show>> AddAsync(Show show, CancellationToken cancellationToken)
        => databaseContext.Shows.AddAsync(show, cancellationToken);

    public Task AddRangeAsync(IEnumerable<Show> shows, CancellationToken cancellationToken)
        => databaseContext.Shows.AddRangeAsync(shows, cancellationToken);

    public Task<bool> AnyAsync(int showId) => databaseContext.Shows.AnyAsync(show => show.Id == showId);

    public Task<Show?> Details(int showId) => databaseContext.Shows.FirstOrDefaultAsync(show => show.Id == showId);

    public Task<int> RemoveByIdAsync(int showId, CancellationToken cancellationToken)
        => databaseContext.Shows.Where(show => show.Id == showId).ExecuteDeleteAsync(cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => databaseContext.SaveChangesAsync(cancellationToken);

    public void UpdateAsync(Show show) => databaseContext.Entry(show).State = EntityState.Modified;
}
