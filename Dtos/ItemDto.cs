using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record ItemDto(
        string Id, 
        string InStore, 
        MultiLanguageString Name, 
        string Type,
        string Cost, 
        string CostType, 
        string Xp, 
        string? Groups,
        string Trains, 
        string UpgradesTo, 
        string DisplayOrder,
        string Activation, 
        string Expiration,
        string Collect, 
        string CollectType, 
        string CollectXp,
        string CategoryId, 
        string SubcategoryId, 
        string SubcatFunctional,
        string MinLevel, 
        string Width, 
        string Height, 
        string MaxFrame,
        string Giftable, 
        string ImgName, 
        string Elevation, 
        string UnitCapacity,
        string Attack, 
        string Defense, 
        string Life, 
        string Velocity, 
        string AttackRange, 
        string AttackInterval,
        string NewItem, 
        string Population, 
        string GiftLevel, 
        string CostUnitCash, 
        string Race, 
        string Flying,
        string Protect, 
        string Potion, 
        string Achievement, 
        MultiLanguageString AchievementDesc, 
        string UnitsLimit,
        string? StoreGroups, 
        string StoreLevel, 
        string Size,
        string ShowOnMobile, 
        string ShowOnMobileStore, 
        string OnlyMobile, 
        string? IphoneAdjustments);

    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.InStore, opt => opt.MapFrom(src => src.InStore == "1"))
                .ForMember(dest => dest.Giftable, opt => opt.MapFrom(src => src.Giftable == "1"))
                .ForMember(dest => dest.NewItem, opt => opt.MapFrom(src => src.NewItem == "1"))
                .ForMember(dest => dest.Flying, opt => opt.MapFrom(src => src.Flying == "1"))
                .ForMember(dest => dest.Protect, opt => opt.MapFrom(src => src.Protect == "1"))
                .ForMember(dest => dest.Achievement, opt => opt.MapFrom(src => src.Achievement == "1"));

            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.InStore, opt => opt.MapFrom(src => src.InStore == true ? "1" : "0"))
                .ForMember(dest => dest.Giftable, opt => opt.MapFrom(src => src.Giftable == true ? "1" : "0"))
                .ForMember(dest => dest.NewItem, opt => opt.MapFrom(src => src.NewItem == true ? "1" : "0"))
                .ForMember(dest => dest.Flying, opt => opt.MapFrom(src => src.Flying == true ? "1" : "0"))
                .ForMember(dest => dest.Protect, opt => opt.MapFrom(src => src.Protect == true ? "1" : "0"))
                .ForMember(dest => dest.Achievement, opt => opt.MapFrom(src => src.Achievement == true ? "1" : "0"));
        }
    }
}
