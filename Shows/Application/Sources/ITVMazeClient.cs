using Domain.ApiModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shows.Api.Application.Config;

namespace Shows.Api.Application.Sources;

/// <summary>
/// Represents (some) of the api calls for TVMaze API
/// </summary>
public interface ITVMazeClient
{
    /// <summary>
    /// Get shows overview by page
    /// </summary>
    /// <param name="pageNumber">Page number for shows</param>
    /// <returns>List of shows</returns>
    Task<IEnumerable<ShowApiModel>> ShowsAsync(int pageNumber);
}

/// <summary>
/// Implementation of the ITVMazeClient
/// </summary>
/// <param name="httpClient">HTTPClient</param>
/// <param name="config">Maze config</param>
public sealed class TVMazeClient(HttpClient httpClient, IOptions<TVMazeConfig> config) : ITVMazeClient
{
    public async Task<IEnumerable<ShowApiModel>> ShowsAsync(int pageNumber)
    {
        using (httpClient)
        {
            // Add UserAgent headers to identify ourselves
            httpClient.DefaultRequestHeaders.UserAgent.Add(new("XPrtzMazeClient", "1.0"));
            
            HttpResponseMessage response = await httpClient.GetAsync($"{config.Value.RootUrl}shows?page={pageNumber}");
            string responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<IEnumerable<ShowApiModel>>(responseBody);
        }
    }
}