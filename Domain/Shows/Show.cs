using Microsoft.EntityFrameworkCore;

namespace Domain.Shows;

/// <summary>
/// Represents a TV Show
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="language"></param>
/// <param name="premiered"></param>
/// <param name="summary"></param>
/// <param name="genres"></param>
/// <remarks>
/// All properties are taken from the assignment.
/// The id is determined and provided by an external api (https://www.tvmaze.com)
/// </remarks>
public class Show(int id, string name, string language, DateOnly? premiered, string summary, List<string> genres)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Language { get; private set; } = language;
    public DateOnly? Premiered { get; private set; } = premiered;
    public string Summary { get; private set; } = summary;

    public List<string> Genres { get; private set; } = genres;

    /// <summary>
    /// Update the show
    /// </summary>
    /// <param name="name"></param>
    /// <param name="language"></param>
    /// <param name="premiered"></param>
    /// <param name="summary"></param>
    /// <param name="genres"></param>
    /// <returns>DomainResult</returns>
    public DomainResult Update(string name, string language, DateOnly? premiered, string summary, List<string> genres)
    {
        // Todo; add validation for each of the properties and return (multiple) domainresult(s) when validation fails

        Name = name;
        Language = language;
        Premiered = premiered;
        Summary = summary;
        Genres = genres;

        return new DomainResult(DomainResultReason.Valid, string.Empty);
    }
}
