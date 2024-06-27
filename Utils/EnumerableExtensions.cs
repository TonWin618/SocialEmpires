namespace SocialEmpires.Utils
{
    public static class EnumerableExtensions
    {
        public static bool NullRespectingSequenceEqual<T>(this IEnumerable<T>? first, IEnumerable<T>? second)
        {
#pragma warning disable IDE0075
            return first == null && second == null ? true
                 : first == null || second == null ? false
                 : first.SequenceEqual(second);
#pragma warning restore IDE0075
        }
    }
}
