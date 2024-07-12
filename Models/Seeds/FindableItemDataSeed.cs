using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class FindableItemDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public FindableItemDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.FindableItems.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FindableItemDto, FindableItem>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<FindableItem, FindableItemDto>("findable_items", _appDbContext, mapper);
        }

        public record FindableItemDto(int Id, MultiLanguageString Title, MultiLanguageString Description, int Coins);
    }
}
