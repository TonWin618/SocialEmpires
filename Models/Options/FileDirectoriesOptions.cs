namespace SocialEmpires.Models.Options
{
    public class FileDirectoriesOptions
    {
        public string Configs { get; init; } = null!;
        public string Assets { get; init; } = null!;
        public string Uploads { get; init; } = null!;
        public string Maps { get; init; } = null!;

        public FileDirectoriesOptions()
        {
            //for configuration options.
        }
    }
}
