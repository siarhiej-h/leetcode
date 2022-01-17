using System;
using System.Linq;

Console.WriteLine(Trap(new [] { 4, 4, 4, 7, 1, 0 }));

static (int result, bool found) FindFirst(int start, int end, Func<int, bool> predicate)
{
    for (int i = start; i != end; i++)
    {
        if (predicate(i))
            return (i, true);
    }
    return (-1, false);
}

static (Range range, bool trapFound, bool endReached) FindNextTrap(int[] heights, int start)
{
    if (start >= heights.Length - 1)
        return (Range.EndAt(0), false, true);

    var (trapStart, startFound) = FindFirst(start, heights.Length - 1, i => heights[i + 1] < heights[i]);
    if (!startFound)
        return (Range.EndAt(0), false, true);

    var (trapEnd, trapEndFound) = FindFirst(trapStart + 1, heights.Length, i => heights[i] >= heights[trapStart]);
    if (!trapEndFound)
    {
        var span = heights.AsSpan(trapStart);
        span.Reverse();
        return FindNextTrap(heights, trapStart);
    }

    return (trapStart..trapEnd, true, false);
}

static int Trap(int[] heights)
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
