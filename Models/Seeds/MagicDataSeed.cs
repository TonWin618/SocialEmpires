using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class MagicDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public MagicDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Magics.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MagicDto, Magic>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<Magic, MagicDto>("magics", _appDbContext, mapper);
        }

        public record MagicDto(
            int Id,
            MultiLanguageString Name,
            MultiLanguageString Description,
            int Mana,
            string Area,
            int Level,
            int Gold,
            int Cash,
            string ImgName,
            int Target
        );
    }
}
