namespace SocialEmpires.Models;

public class PlayerInfo
{
    /// <summary>
    /// player id
    /// </summary>
    public string Pid { get; set; }

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

    private PlayerInfo()
    {
        // for EF Core
    }

    public static PlayerInfo Create(string playerId, string name)
    {
        return new PlayerInfo()
        {
            Pid = playerId,
            Name = name,
            Pic = playerId,
            Cash = 0,
            CompletedTutorial = 0,
            DefaultMap = 0,
            MapNames = new List<string>() { "My Empire" },
            MapSizes = new List<int>() { 0 },
            WorldId = 0,
            LastLoggedIn = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
    }
}
