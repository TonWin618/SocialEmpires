using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{

    public class SocialItemDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public SocialItemDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.SocialItems.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SocialItemDto, SocialItem>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<SocialItem, SocialItemDto>("social_items", _appDbContext, mapper);
        }

        public record SocialItemDto(int Id, int WorkerCost, MultiLanguageString Workers);
    }
}
