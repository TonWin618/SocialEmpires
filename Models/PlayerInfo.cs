namespace SocialEmpires.Models;

public record PlayerInfo
{
    public string Pid { get; init; }
    public string Name { get; init; }
    public string Pic { get; init; }
    public int Cash { get; init; }
    public int CompletedTutorial { get; init; }
    public int DefaultMap { get; init; }
    public List<string> MapNames { get; init; }
    public List<int> MapSizes { get; init; }
    public int WorldId { get; init; }
    public int SpRefUid { get; init; }
    public string SpRefCatInstall { get; init; }
    public long LastLoggedIn { get; init; }
}
