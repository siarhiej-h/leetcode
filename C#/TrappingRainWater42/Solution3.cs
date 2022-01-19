public class Solution3
{
    static (Range range, bool trapFound, bool endReached) FindNextTrap(int[] heights, int start)
    {
        if (start >= heights.Length - 1)
            return (Range.EndAt(0), false, true);

        var (trapStart, startFound) = Enumerable.Range(start, heights.Length - 1 - start)
            .Where(i => heights[i + 1] < heights[i])
            .Select(i => (i, true))
            .FirstOrDefault();
        if (!startFound)
            return (Range.EndAt(0), false, true);

        var (trapEnd, trapEndFound) = Enumerable.Range(trapStart + 1, heights.Length - start)
            .Where(i => heights[i] >= heights[trapStart])
            .Select(i => (i, true))
            .FirstOrDefault();

        if (!trapEndFound)
        {
            var span = heights.AsSpan(trapStart);
            span.Reverse();
            return FindNextTrap(heights, trapStart);
        }

        return (trapStart..trapEnd, true, false);
    }

    public int Trap(int[] heights)
    {
        if (heights.Length < 3)
            return 0;

        int waterVolume = 0;

        Range range = ..0;
        bool finished = false;
        bool trapFound = false;
        while (!finished)
        {
            if (trapFound)
            {
                var trapHeight = Math.Min(heights[range.Start.Value], heights[range.End.Value]);
                waterVolume += heights[range]
                    .Select(height => Math.Max(0, trapHeight - height))
                    .Sum();
            }

            (range, trapFound, finished) = FindNextTrap(heights, range.End.Value);
        }

        return waterVolume;
    }
}