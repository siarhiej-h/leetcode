public class Solution
{
    public int NumberOfWays(string corridor)
    {
        int seats = corridor.Count(ch => ch == 'S');
        if (seats % 2 != 0 || seats == 0)
            return 0;

        long basis = ((long)1e9) + 7;
        return (int) GetMultipliers(corridor).Aggregate(1L, (a, b) => (a * b) % basis);
    }

    private static IEnumerable<int> GetMultipliers(string corridor)
    {
        var start = corridor.IndexOf('S', corridor.IndexOf('S') + 1) + 1;
        while (start < corridor.Length)
        {
            int plants = Enumerable.Range(start, corridor.Length - start - 1)
                .TakeWhile(i => corridor[i] == 'P')
                .Count();

            start = corridor.IndexOf('S', start + plants);
            if (start == -1)
                yield break;

            yield return plants + 1;
            start = corridor.IndexOf('S', start + 1) + 1;
        }
    }
}
