namespace SocialEmpires.Models;

public record PlayerInfo
{
    /// <summary>
    /// player id
    /// </summary>
    public int Pid { get; set; }
    /// <summary>
    /// player name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// player avatar
    /// </summary>
    public string Pic { get; set; }

    public int Cash { get; set; }
    public int CompletedTutorial { get; set; }

    public int DefaultMap { get; set; }
    public List<string> MapNames { get; set; }
    public List<int> MapSizes { get; set; }

    public int WorldId { get; set; }
    public long LastLoggedIn { get; set; }
}
