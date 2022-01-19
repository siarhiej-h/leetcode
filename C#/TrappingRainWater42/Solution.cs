public class Solution
{
    private (int sum, int leftBoundary) CalculateForward(int[] heights)
    {
        var (segmentStart, startFound) = Enumerable.Range(0, heights.Length - 2)
            .Where(i => heights[i + 1] < heights[i])
            .Select(i => (i, found: true))
            .FirstOrDefault();

        if (startFound)
        {
            var sum = Enumerable.Range(segmentStart + 1, heights.Length - segmentStart - 1)
                .Where(i => heights[i] >= heights[segmentStart])
                .Sum(segmentEnd => CalculateSegmentSum(heights, segmentStart, segmentEnd, heights[segmentStart],
                    () => segmentStart = segmentEnd));
            return (sum, segmentStart);
        }

        return (0, 0);
    }

    private int CalculateBackward(int[] heights, int leftBoundary)
    {
        if (leftBoundary > heights.Length - 3)
            return 0;

        var (segmentEnd, endFound) = Enumerable.Range(leftBoundary + 2, heights.Length - leftBoundary - 2)
            .Reverse()
            .Where(i => heights[i - 1] < heights[i])
            .Select(i => (i, found: true))
            .FirstOrDefault();

        if (endFound)
        {
            return Enumerable.Range(leftBoundary, segmentEnd - 1 - leftBoundary)
                .Reverse()
                .Where(i => heights[i] >= heights[segmentEnd])
                .Sum(segmentStart => CalculateSegmentSum(heights, segmentStart, segmentEnd, heights[segmentEnd],
                    () => segmentEnd = segmentStart));
        }

        return 0;
    }

    private static int CalculateSegmentSum(int[] heights, int start, int end, int trapHeight, Action sideEffect)
    {
        var sum = heights
            .Skip(start + 1)
            .Take(end - start - 1)
            .Sum(height => trapHeight - height);
        sideEffect();
        return sum;
    }

    public int Trap(int[] heights)
    {
        if (heights.Length < 3)
            return 0;

        var (trappedForward, leftBoundary) = CalculateForward(heights);
        var trappedBackward = CalculateBackward(heights, leftBoundary);
        return trappedForward + trappedBackward;
    }
}
