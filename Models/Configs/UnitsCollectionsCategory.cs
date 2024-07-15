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

        public List<int>? Costs { get; set; }

        public int Position {  get; set; }
    }
}
