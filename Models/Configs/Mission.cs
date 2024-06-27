namespace SocialEmpires.Models.Configs
{
    public class Mission
    {
        public string Description { get; init; } = null!;
        public string Hint { get; init; } = null!;
        public int Reward { get; init; }
        public int Id { get; init; }
        public string Title { get; init; } = null!;
    }
}
