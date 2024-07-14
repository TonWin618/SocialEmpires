using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record MagicDto(
            int Id,
            MultiLanguageString Name,
            MultiLanguageString Description,
            int Mana,
            string Area,
            int Level,
            int Gold,
            int Cash,
            string ImgName,
            int Target
        );

    public class MagicProfile : Profile
    {
        public MagicProfile()
        {
            CreateMap<MagicDto, Magic>().ReverseMap(); ;
        }
    }
}
