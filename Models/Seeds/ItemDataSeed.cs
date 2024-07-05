using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public static class ItemDataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider, IEnumerable<Item> items)
        {
            using (var DbContext = serviceProvider.GetRequiredService<AppDbContext>())
            {
                if(DbContext.Items.Any())
                {
                    return;
                }

                DbContext.AddRange(items);

                DbContext.SaveChanges();
            }
        }
    }
}
