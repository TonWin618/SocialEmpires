using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Item, Item>();
        }
    }
}
