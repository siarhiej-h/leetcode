public class Solution
{
    private int CalculateForward(int[] heights, out int leftBoundary)
    {
        var segmentStart = Enumerable.Range(0, heights.Length - 2)
            .Select((h, i) => (int?) i)
            .TakeWhile(i => i.Value < heights.Length - 1)
            .FirstOrDefault(i => heights[i.Value + 1] < heights[i.Value]);

        if (segmentStart.HasValue)
        {
            (int segmentStart, int segmentLength, int trapHeight) SelectSegment(int segmentEnd)
            {
                var segment = (segmentStart.Value + 1, segmentEnd - segmentStart.Value - 1, heights[segmentStart.Value]);
                segmentStart = segmentEnd;
                return segment;
            }

            var segments = heights.Select((h, i) => i)
                .Skip(segmentStart.Value + 1)
                .Where(i => heights[i] >= heights[segmentStart.Value])
                .Select(SelectSegment);
            var sum = SumSegments(heights, segments);
            leftBoundary = segmentStart.Value;
            return sum;
        }

        leftBoundary = 0;
        return 0;
    }

    private int CalculateBackward(int[] heights, int segmentStart)
    {
        int? segmentEnd = heights
            .Select((h, i) => (int?) heights.Length - 1 - i)
            .TakeWhile(i => i.Value > segmentStart + 1)
            .FirstOrDefault(i => heights[i.Value - 1] < heights[i.Value]);

        if (segmentEnd.HasValue)
        {
            (int segmentStart, int segmentLength, int trapHeight) SelectSegment(int segmentStart)
            {
                var segment = (segmentStart + 1, segmentEnd.Value - segmentStart - 1, heights[segmentEnd.Value]);
                segmentEnd = segmentStart;
                return segment;
            }

            var segments = Enumerable.Range(segmentStart, segmentEnd.Value - 1 - segmentStart)
                .Reverse()
                .Where(i => heights[i] >= heights[segmentEnd.Value])
                .Select(SelectSegment);
            return SumSegments(heights, segments);
        }

        return 0;
    }

    private int SumSegments(int[] heights, IEnumerable<(int segmentStart, int segmentLength, int trapHeight)> segments)
    {
        return segments.SelectMany(segment => heights
            .Skip(segment.segmentStart)
            .Take(segment.segmentLength)
            .Select(height => segment.trapHeight - height))
            .Sum();
    }

    public int Trap(int[] heights)
    {
        if (heights.Length < 3)
            return 0;
        return CalculateForward(heights, out var leftBoundary) + CalculateBackward(heights, leftBoundary);
    }
}
