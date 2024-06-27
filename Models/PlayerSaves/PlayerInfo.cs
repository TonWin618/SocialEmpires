using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models;

public class PlayerInfo
{
    /// <summary>
    /// player id
    /// </summary>
    public string Pid { get; set; } = null!;

    /// <summary>
    /// player name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// player avatar
    /// </summary>
    public string Pic { get; set; } = null!;

    public int Cash { get; set; }
    public int CompletedTutorial { get; set; }

    public int DefaultMap { get; set; }
    public List<string> MapNames { get; set; } = null!;
    public List<int> MapSizes { get; set; } = null!;

    public int WorldId { get; set; }
    public long LastLoggedIn { get; set; }

    [NotMapped]
    public int Wood { get; } = 0;
    [NotMapped]
    public int Coin { get; } = 0;
    [NotMapped]
    public int Stone { get; } = 0;
    [NotMapped]
    public int Food { get; } = 0;
    [NotMapped]
    public int Xp { get; } = 0;
    [NotMapped]
    public int Level { get; } = 0;
    [NotMapped]
    public string SpRefCatInstall { get; } = "ts";
    [NotMapped]
    public long SpRefUid { get; } = 1000;
    [NotMapped]
    [JsonPropertyName("__#__coins")]
    public int Coins { get; } = 0;
    [NotMapped]
    [JsonPropertyName("__#__level")]
    public int Levels { get; } = 0;
    [NotMapped]
    [JsonPropertyName("__#__xp")]
    public int Xps { get; } = 0;


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
