public static class EnumerableExtensions
{
    public static IEnumerable<T> AppendRange<T>(this IEnumerable<T> source, IEnumerable<T> items)
    {
        return source.Concat(items);
    }
}