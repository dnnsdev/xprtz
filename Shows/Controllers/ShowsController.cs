using Domain.ApiModels;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Application.CommandHandlers;
using Shows.Api.Application.Queries;

namespace Shows.Api.Controllers;

/// <summary>
/// Shows controller
/// </summary>
/// <param name="mediatr">Mediator</param>
/// <param name="showQueries">Show queries</param>
[ApiController]
[Route("[controller]/[action]")]
public sealed class ShowsController(IMediator mediatr, IShowQueries showQueries) : Controller
{
    /// <summary>
    /// Show details (by name)
    /// </summary>
    /// <param name="name">Name of show</param>
    /// <returns>200 on success, 404 on not exists</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Details([FromQuery] string name)
    {
        ShowApiModel? showDetails = await showQueries.DetailsByName(name);

        if (showDetails is null)
            return NotFound();

        return Ok(showDetails);
    }
    
    /// <summary>
    /// Update a show
    /// </summary>
    /// <param name="model">Show Api model</param>
    /// <returns>200 on success, 404 on not exists</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update([FromBody] ShowUpdateCommand model)
        => (IActionResult)mediatr.Send(model);

    /// <summary>
    /// Remove a show by show id
    /// </summary>
    /// <param name="id">Show id</param>
    /// <returns>200 on success</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Delete([FromQuery] int id)
        => (IActionResult)mediatr.Send(new DeleteShowCommand(id));
}