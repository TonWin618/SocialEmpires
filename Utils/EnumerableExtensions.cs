namespace SocialEmpires.Utils
{
    public static class EnumerableExtensions
    {
        public static bool NullRespectingSequenceEqual<T>(this IEnumerable<T>? first, IEnumerable<T>? second)
        {
            return first == null && second == null ? true
                 : first == null || second == null ? false
                 : first.SequenceEqual(second);
        }
    }
}
