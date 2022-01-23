public class Solution
{
    public int MaximumGood(int[][] statements)
    {
        var n = statements.Length;
        int maxCombinations = (int) Math.Pow(2, n);
        int max = 0;
        for (int mask = maxCombinations - 1; mask >= 0; mask--)
        {
            var match = Enumerable.Range(0, n)
                .Where(index => IsGood(mask, index))
                .All(index => CheckStatements(mask, statements[index]));
            
            if (match)
            {
                var goodPeople = Enumerable.Range(0, n).Count(index => IsGood(mask, index));
                max = Math.Max(max, goodPeople);
            }
        }
        return max;
    }

    private static bool CheckStatements(int mask, int[] statements)
    {
        return statements
            .Select((s, i) => (statement: s, index: i))
            .All(t => t.statement == 2 || (t.statement == 1) == IsGood(mask, t.index));
    }

    private static bool IsGood(int mask, int index)
    {
        return (mask & (1 << index)) != 0;
    }
}