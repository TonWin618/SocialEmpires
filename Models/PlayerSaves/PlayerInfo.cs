namespace SocialEmpires.Models;

public record PlayerInfo
{
    /// <summary>
    /// player id
    /// </summary>
    public int Pid { get; init; }
    /// <summary>
    /// player name
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// player avatar
    /// </summary>
    public string Pic { get; init; }

    public int Cash { get; init; }
    public int CompletedTutorial { get; init; }

    public int DefaultMap { get; init; }
    public List<string> MapNames { get; init; }
    public List<int> MapSizes { get; init; }

    public int WorldId { get; init; }
    public long LastLoggedIn { get; init; }
}
