public class Solution2
{
    private static IEnumerable<(int segmentStart, int segmentEnd, int trapHeight)> FindSegments(int[] heights)
    {
        int? segmentStart = heights
            .Select((h, i) => (int?)i)
            .TakeWhile(i => i.Value < heights.Length - 1)
            .FirstOrDefault(i => heights[i.Value + 1] < heights[i.Value]);

        if (segmentStart.HasValue)
        {
            for (int i = segmentStart.Value + 1; i != heights.Length; i++)
            {
                if (heights[i] >= heights[segmentStart.Value])
                {
                    yield return (segmentStart.Value + 1, i, heights[segmentStart.Value]);
                    segmentStart = i;
                }
            }
        }

        if (segmentStart.GetValueOrDefault(0) > heights.Length - 3)
            yield break;

        int? segmentEnd = heights
            .Reverse()
            .Select((h, i) => (int?)heights.Length - 1 - i)
            .TakeWhile(i => i.Value > segmentStart.GetValueOrDefault(0) + 1)
            .FirstOrDefault(i => heights[i.Value - 1] < heights[i.Value]);

        if (segmentEnd.HasValue)
        {
            for (int i = segmentEnd.Value - 1; i >= segmentStart.GetValueOrDefault(0); i--)
            {
                if (heights[i] >= heights[segmentEnd.Value])
                {
                    yield return (i + 1, segmentEnd.Value, heights[segmentEnd.Value]);
                    segmentEnd = i;
                }
            }
        }
    }

    public int Trap(int[] height)
    {
        return FindSegments(height)
            .Select(t => height[t.segmentStart..t.segmentEnd]
            .Sum(height => t.trapHeight - height))
            .Sum();
    }
}