using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
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
    }
}
