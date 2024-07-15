using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record UnitsCollectionsCategoryDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int CategoryLangId { get; set; }

        public List<int> Units { get; set; }

        public int Rewards { get; set; }

        public int Cost { get; set; }

        public List<int>? Costs { get; set; }

        public int Position { get; set; }
    }

    public class UnitsCollectionsCategoryProfile : Profile
    {
        public UnitsCollectionsCategoryProfile()
        {
            CreateMap<UnitsCollectionsCategoryDto, UnitsCollectionsCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap(); ;
        }
    }
}
