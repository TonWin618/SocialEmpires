using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class ItemDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public ItemDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Items.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemDto, Item>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
                    .ForMember(dest => dest.Xp, opt => opt.MapFrom(src => int.Parse(src.Xp)));
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<Item, ItemDto>("items", _appDbContext, mapper);
        }
    }
}
