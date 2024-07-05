using SocialEmpires.Models.Configs;
using SocialEmpires.Services;

namespace SocialEmpires.Models.Seeds
{
    public static class ItemDataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var DbContext = serviceProvider.GetRequiredService<AppDbContext>())
            {
                if(DbContext.Items.Any())
                {
                    return;
                }

                var configFileService = serviceProvider.GetRequiredService<ConfigFileService>();
                var items = configFileService.Items;

                DbContext.AddRange(items);
                DbContext.SaveChanges();
            }
        }
    }
}
