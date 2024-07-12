using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class NeighborAssistDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public NeighborAssistDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.NeighborAssists.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NeighborAssistDto, NeighborAssist>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<NeighborAssist, NeighborAssistDto>("neighbor_assists", _appDbContext, mapper);
        }

        public record NeighborAssistDto(
            Reward Reward, 
            int Rnd, 
            MultiLanguageString Task, 
            MultiLanguageString Action, 
            MultiLanguageString Notification);
    }
}
