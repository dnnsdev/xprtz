namespace Domain.ApiModels;

/// <summary>
/// Show API Model.
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="language"></param>
/// <param name="premiered"></param>
/// <param name="summary"></param>
/// <param name="genres"></param>
/// <remarks>Used for read-only purposes</remarks>
public sealed class ShowApiModel(int id, string name, string language, DateOnly? premiered, string summary, List<string> genres)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Language { get; private set; } = language;
    public DateOnly? Premiered { get; private set; } = premiered;
    public string Summary { get; private set; } = summary;
    public List<string> Genres { get; private set; } = genres;
}
