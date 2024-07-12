namespace SocialEmpires.Dtos
{
    public record DartsItemDto(
            int Id,
            string StartDate,
            List<int> Items,
            int ExtraItem
        );
}
