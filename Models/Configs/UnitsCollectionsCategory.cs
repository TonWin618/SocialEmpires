using Microsoft.IdentityModel.Tokens;

namespace SocialEmpires.Models.Configs
{
    public class UnitsCollectionsCategory
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int CategoryLangId { get; set; }

        public List<int> Units {  get; set; }

        public int Rewards {  get; set; }

        public int Cost {  get; set; }

        private List<int>? costs;
        public List<int>? Costs {
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

        public int Position {  get; set; }
    }
}
