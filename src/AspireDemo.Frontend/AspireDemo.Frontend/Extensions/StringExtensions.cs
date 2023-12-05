namespace AspireDemo.Frontend.Extensions;
public static class StringExtensions
{
    public static string ReplaceLastOccurrence(this string source, string find, string replace)
    {
        int place = source.LastIndexOf(find, StringComparison.InvariantCultureIgnoreCase);

        if (place == -1)
        {
            return source;
        }

        return source.Remove(place, find.Length).Insert(place, replace);
    }
}