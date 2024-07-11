using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class LevelDataSeed
    {
        private readonly AppDbContext _appDbContext;
        public LevelDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Levels.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LevelDto, Level>()
                .ForMember(
                    dest => dest.ToLevel, 
                    opt=> { opt.MapFrom(src => MappingCounter.Count + 1); })
                .AfterMap((src,dest) => MappingCounter.Increment());
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<Level, LevelDto>("levels", _appDbContext, mapper);
        }

        private record LevelDto(string RewardType, int ExpRequired, MultiLanguageString Name, int RewardAmount);
    }

    

    internal static class MappingCounter
    {
        private static int _count = 0;

        public static int Count => _count;

        public static void Increment() => _count++;
    }
}
