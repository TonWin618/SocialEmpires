using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record LocalizationStringDto(int Id, string Name, MultiLanguageString Text);

    public class LocalizationStringProfile : Profile
    {
        public LocalizationStringProfile()
        {
            CreateMap<LocalizationStringDto, LocalizationString>().ReverseMap(); ;
        }
    }
}
