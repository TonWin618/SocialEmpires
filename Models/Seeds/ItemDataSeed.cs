using SocialEmpires.Services;

namespace SocialEmpires.Models.Seeds
{
    public static class ItemDataSeed
    {
        public static void Initialize(ConfigFileService configFileService, AppDbContext dbContext)
        {
            if(dbContext.Items.Any())
            {
                return;
            }

            var items = configFileService.Items;

            dbContext.AddRange(items);
            dbContext.SaveChanges();
        }
    }
}
