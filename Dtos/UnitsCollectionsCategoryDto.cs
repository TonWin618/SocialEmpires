using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public class UnitsCollectionsCategoryDto
    {
        public int CategoryId { get; set; }

        public int CategoryLangId { get; set; }

        public List<int> Units { get; set; } = null!;

        public int Rewards { get; set; }

        public int Cost { get; set; }

        private List<int>? costs;
        public List<int>? Costs
        {
            get
            {
                return costs;
            }
            set
            {
                //TODO: AutoMapper map behavior
                //Prevent AutoMapper from converting null to an empty list.
                if (value.IsNullOrEmpty())
                {
                    value = null;
                }
                else
                {
                    costs = value;
                }
            }
        }

        public int Position { get; set; }
    }

    public class UnitsCollectionsCategoryProfile : Profile
    {
        public UnitsCollectionsCategoryProfile()
        {
            CreateMap<UnitsCollectionsCategoryDto, UnitsCollectionsCategory>()
                .ReverseMap();
        }
    }
}
