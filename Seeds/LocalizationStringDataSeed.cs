using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class LocalizationStringDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LocalizationStringDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.LocalizationStrings.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<LocalizationString, LocalizationStringDto>("localization_strings", _appDbContext, _mapper);
        }
    }
}
