using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class LocalizationStringDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public LocalizationStringDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.LocalizationStrings.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocalizationStringDto, LocalizationString>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<LocalizationString, LocalizationStringDto>("localization_strings", _appDbContext, mapper);
        }

        private record LocalizationStringDto(int Id, MultiLanguageString Name, MultiLanguageString Text);
    }
}
