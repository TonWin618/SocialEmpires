using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public MultiLanguageString Name { get; set; } = null!;

        public List<SubCategoryDto> Sub { get; set; } = null!;
    }

    public class SubCategoryDto
    {
        public int Id { get; set; }

        public MultiLanguageString Name { get; set; } = null!;

        public int Parent { get; set; }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
        }
    }
}
