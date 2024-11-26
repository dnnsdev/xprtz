using MediatR;
using Shows.Api.Application.CommandHandlers;

namespace Shows.Api.Application.BackgroundService;

/// <summary>
/// Populate series in background
/// </summary>
/// <param name="mediator">Mediator</param>
/// <param name="logger">Logger</param>
public sealed class ShowsPopulationService(IMediator mediator, ILogger<ShowsPopulationService> logger) : Microsoft.Extensions.Hosting.BackgroundService
{
    /// <summary>
    /// This needs to be smarter, but works as long as there are no records
    /// </summary>
    private int _pageNumber = 0;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Timed Background Service running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Timed Background Service is working.");

            await mediator.Send(new PopulateShowsCommand(_pageNumber), stoppingToken);
            
            // RecurringJob.AddOrUpdate(
            //     "populateShows",
            //     () => Console.WriteLine("Todo:"),
            //     Cron.Hourly);

            _pageNumber++;
            
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        logger.LogInformation("Timed Background Service is stopping.");
    }
}