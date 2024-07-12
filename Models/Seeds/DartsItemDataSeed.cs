using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class DartsItemDataSeed: IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public DartsItemDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.DartsItems.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DartsItemDto, DartsItem>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<DartsItem, DartsItemDto>("darts_items", _appDbContext, mapper);
        }

        public record DartsItemDto(
            int Id,
            string StartDate,
            List<int> Items,
            int ExtraItem
        );
    }
}
