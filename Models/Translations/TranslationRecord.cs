namespace SocialEmpires.Models.Translations
{
    public class TranslationRecord
    {
        public int Id { get; set; }

        public int ItemId {  get; set; }
        public string Section { get; set; } = null!;
        public string Property { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Translation { get; set; } = null!;

        //null if anonymous
        public string? SubmitterId {  get; set; }

        public string? ApproverId {  get; set; }
        public bool Approved {  get; set; }
    }
}
