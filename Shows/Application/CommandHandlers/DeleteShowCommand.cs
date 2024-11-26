using Domain.Shows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Application.CommandHandlers;

/// <summary>
/// Delete show command
/// </summary>
/// <param name="showId">Show id</param>
public sealed class DeleteShowCommand(int showId) : IRequest<IActionResult>
{
    public int ShowId { get; set; } = showId;
}

/// <summary>
/// Delete show command handler
/// </summary>
/// <param name="showRepository">Show repository</param>
public sealed class DeleteShowCommandHandler(IShowRepository showRepository) : IRequestHandler<DeleteShowCommand, IActionResult>
{
    public async Task<IActionResult> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
    {
        await showRepository.RemoveByIdAsync(request.ShowId, cancellationToken);

        return new OkResult();
    }
}
