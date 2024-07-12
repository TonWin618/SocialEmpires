using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class HonorLevelDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public HonorLevelDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.HonorLevels.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HonorLevelDto, HonorLevel>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<HonorLevel, HonorLevelDto>("honor_levels", _appDbContext, mapper);
        }

        public record HonorLevelDto(
            int Id,
            int Points,
            MultiLanguageString Rank
        );
    }
}
