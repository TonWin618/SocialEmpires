using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class NeighborAssistDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public NeighborAssistDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.NeighborAssists.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<NeighborAssist, NeighborAssistDto>("neighbor_assists", _appDbContext, _mapper);
        }
    }
}
