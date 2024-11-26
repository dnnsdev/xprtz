using Domain.Shows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Application.CommandHandlers;

/// <summary>
/// Command to update a single show
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="language"></param>
/// <param name="premiered"></param>
/// <param name="summary"></param>
/// <param name="genres"></param>
public sealed class ShowUpdateCommand(int id, string name, string language, DateOnly premiered, string summary, List<string> genres)
    : IRequest<IActionResult>
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Language { get; private set; } = language;
    public DateOnly Premiered { get; private set; } = premiered;
    public string Summary { get; private set; } = summary;

    public List<string> Genres { get; private set; } = genres;
}

/// <summary>
/// Update a single show handler
/// </summary>
/// <param name="showRepository">Show repository</param>
public sealed class UpdateShowHandler(IShowRepository showRepository) : IRequestHandler<ShowUpdateCommand, IActionResult>
{
    public async Task<IActionResult> Handle(ShowUpdateCommand request, CancellationToken cancellationToken)
    {
        // TODO: This should be moved to ValidationBehavior for each of the commands
        bool showExists = await showRepository.AnyAsync(request.Id);
        if (!showExists)
            return new NotFoundResult();

        Show? show = await showRepository.Details(request.Id);
        if (show is null)
            return new NotFoundResult();

        show.Update(request.Name, request.Language, request.Premiered, request.Summary, request.Genres);

        // Mark as updated
        showRepository.UpdateAsync(show);
        
        // Persist to database
        // TODO: This should happen for each of the request and could be done in a pipeline solution
        await showRepository.SaveChangesAsync(cancellationToken);

        return new OkResult();
    }
}
