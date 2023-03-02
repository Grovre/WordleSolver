namespace WordleApi.helpers;

public static class SpanHelper
{
    public static void Shuffle<T>(this Span<T> span, Random random)
    {
        for (var i = 0; i < span.Length; i++)
        {
            var randomIndex = random.Next(i, span.Length);
            (span[i], span[randomIndex]) = (span[randomIndex], span[i]);
        }
    }
}