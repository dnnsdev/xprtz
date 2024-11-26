using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Shows;

/// <summary>
/// Show(s) repository
/// </summary>
public interface IShowRepository
{
    /// <summary>
    /// Save changes for the whole repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Affected rows</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get show details by id
    /// </summary>
    /// <param name="showId">Show id</param>
    /// <returns>Show</returns>
    public Task<Show?> Details(int showId);

    /// <summary>
    /// Update single show entity
    /// </summary>
    /// <param name="show"></param>
    public void UpdateAsync(Show show);

    /// <summary>
    /// Add range of shows
    /// </summary>
    /// <param name="shows">Shows</param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<Show> shows, CancellationToken cancellationToken);

    /// <summary>
    /// Add a single show
    /// </summary>
    /// <param name="show">Show</param>
    public ValueTask<EntityEntry<Show>> AddAsync(Show show, CancellationToken cancellationToken);

    /// <summary>
    /// Remove a single show
    /// </summary>
    /// <param name="showId">Show id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <remarks>Removes the show by id</remarks>
    public Task<int> RemoveByIdAsync(int showId, CancellationToken cancellationToken);

    /// <summary>
    /// Returns if the show exists by id
    /// </summary>
    /// <param name="showId">Show id</param>
    /// <returns>true when show exists, false when not</returns>
    public Task<bool> AnyAsync(int showId);
}
