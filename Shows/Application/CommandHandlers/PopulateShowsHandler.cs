using Domain.ApiModels;
using Domain.Shows;
using MediatR;
using Shows.Api.Application.Queries;
using Shows.Api.Application.Sources;

namespace Shows.Api.Application.CommandHandlers;

/// <summary>
/// Populate shows command per page
/// </summary>
/// <param name="pageNumber">PageNumber</param>
public sealed class PopulateShowsCommand(int pageNumber) : IRequest<string>
{
    public int PageNumber { get; set; } = pageNumber;
}

/// <summary>
/// Populate shows handler
/// </summary>
/// <param name="serviceScopeFactory"></param>
/// <param name="logger"></param>
public sealed class PopulateShowsHandler(IServiceScopeFactory serviceScopeFactory, ILogger<PopulateShowsHandler> logger) : IRequestHandler<PopulateShowsCommand, string>
{
    private static DateOnly dateOnly = new(2014, 1, 1);
    
    public async Task<string> Handle(PopulateShowsCommand request, CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        
        IShowQueries showQueries = scope.ServiceProvider.GetRequiredService<IShowQueries>();
        IShowRepository repository = scope.ServiceProvider.GetRequiredService<IShowRepository>();
        ITVMazeClient tvMazeClient = scope.ServiceProvider.GetRequiredService<ITVMazeClient>();
        
        // Determine page to start from
        int startingPage = request.PageNumber;
        
        try
        {
            IEnumerable<ShowApiModel> showsPerPage = await tvMazeClient.ShowsAsync(startingPage);
        
            // Business rule, only find shows premiered after 2024/01/01
            foreach (ShowApiModel show in showsPerPage.Where(show =>
                         show.Premiered.HasValue && show.Premiered >= dateOnly))
            {
                Show newShow = new Show(show.Id, show.Name, show.Language, show.Premiered, show.Summary, show.Genres);
        
                await repository.AddAsync(newShow, cancellationToken);
            }
        
            // Persist this page
            await repository.SaveChangesAsync(cancellationToken);
        
            return $"Saved page {startingPage} successfully";
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while populating shows");
            
            return "Failed to populate shows";
        }

        return "ok";
    }

    /// <summary>
    /// Determine page number
    /// </summary>
    /// <param name="maxId"></param>
    /// <returns></returns>
    private static int determinePageNumber(int maxId) => maxId / 250;
}