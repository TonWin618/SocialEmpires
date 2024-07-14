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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
                .ForMember(dest => dest.InStore, opt => opt.MapFrom(src => src.InStore == "1"))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => int.Parse(src.Cost)))
                .ForMember(dest => dest.Xp, opt => opt.MapFrom(src => int.Parse(src.Xp)))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => int.Parse(src.DisplayOrder)))
                .ForMember(dest => dest.Activation, opt => opt.MapFrom(src => float.Parse(src.Activation)))
                .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => int.Parse(src.Expiration)))
                .ForMember(dest => dest.Collect, opt => opt.MapFrom(src => int.Parse(src.Collect)))
                .ForMember(dest => dest.CollectXp, opt => opt.MapFrom(src => int.Parse(src.CollectXp)))
                .ForMember(dest => dest.MinLevel, opt => opt.MapFrom(src => int.Parse(src.MinLevel)))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => int.Parse(src.Width)))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => int.Parse(src.Height)))
                .ForMember(dest => dest.Giftable, opt => opt.MapFrom(src => src.Giftable == "1"))
                .ForMember(dest => dest.Elevation, opt => opt.MapFrom(src => int.Parse(src.Elevation)))
                .ForMember(dest => dest.UnitCapacity, opt => opt.MapFrom(src => int.Parse(src.UnitCapacity)))
                .ForMember(dest => dest.Attack, opt => opt.MapFrom(src => int.Parse(src.Attack)))
                .ForMember(dest => dest.Defense, opt => opt.MapFrom(src => int.Parse(src.Defense)))
                .ForMember(dest => dest.Life, opt => opt.MapFrom(src => int.Parse(src.Life)))
                .ForMember(dest => dest.Velocity, opt => opt.MapFrom(src => int.Parse(src.Velocity)))
                .ForMember(dest => dest.AttackRange, opt => opt.MapFrom(src => int.Parse(src.AttackRange)))
                .ForMember(dest => dest.AttackInterval, opt => opt.MapFrom(src => int.Parse(src.AttackInterval)))
                .ForMember(dest => dest.NewItem, opt => opt.MapFrom(src => src.NewItem == "1"))
                .ForMember(dest => dest.Population, opt => opt.MapFrom(src => int.Parse(src.Population)))
                .ForMember(dest => dest.GiftLevel, opt => opt.MapFrom(src => int.Parse(src.GiftLevel)))
                .ForMember(dest => dest.CostUnitCash, opt => opt.MapFrom(src => int.Parse(src.CostUnitCash)))
                .ForMember(dest => dest.Flying, opt => opt.MapFrom(src => src.Flying == "1"))
                .ForMember(dest => dest.Protect, opt => opt.MapFrom(src => src.Protect == "1"))
                .ForMember(dest => dest.Potion, opt => opt.MapFrom(src => int.Parse(src.Potion)))
                .ForMember(dest => dest.Achievement, opt => opt.MapFrom(src => src.Achievement == "1"))
                .ForMember(dest => dest.UnitsLimit, opt => opt.MapFrom(src => int.Parse(src.UnitsLimit)))
                .ForMember(dest => dest.StoreLevel, opt => opt.MapFrom(src => int.Parse(src.StoreLevel)))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => int.Parse(src.Size)))
                .ReverseMap();
        }
    }
}
