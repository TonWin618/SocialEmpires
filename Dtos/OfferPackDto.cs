namespace SocialEmpires.Dtos
{
    public record OfferPackDto(
            int Id,
            int Position,
            int CostCash,
            int Gold,
            int Stone,
            int Food,
            int Wood,
            int Xp,
            List<int> Items,
            int Mana,
            int Enabled,
            int PackType
        );
}
