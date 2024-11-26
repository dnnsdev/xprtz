using Domain.ApiModels;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Shows.Api.Application.Queries;

/// <summary>
/// Show queries
/// </summary>
public interface IShowQueries
{
    /// <summary>
    /// Get show details by name
    /// </summary>
    /// <param name="name">Show name</param>
    /// <returns>Show name if found, null when not</returns>
    public Task<ShowApiModel?> DetailsByName(string name);

    /// <summary>
    /// Find Max value of show id
    /// </summary>
    /// <returns>Max value of show id</returns>
    public Task<int> MaxId();

    /// <summary>
    /// Are there any shows present?
    /// </summary>
    /// <returns>Returns whether shows are present</returns>
    public Task<bool> Any();
}

public sealed class ShowQueries(ShowContext showContext) : IShowQueries
{
    public Task<ShowApiModel?> DetailsByName(string name)
        => showContext.Shows
            .AsNoTracking()
            .Where(show => show.Name.Contains(name))
            .Select(show => new ShowApiModel(show.Id, show.Name, show.Language, show.Premiered, show.Summary, show.Genres))
            .FirstOrDefaultAsync();

    public Task<int> MaxId()
        => showContext.Shows.MaxAsync(show => show.Id);

    public Task<bool> Any()
        => showContext.Shows.AnyAsync();
}