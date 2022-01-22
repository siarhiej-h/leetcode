public class Solution
{
    public IList<IList<int>> HighestRankedKItems(int[][] grid, int[] pricing, int[] start, int k)
    {
        var (pricingDown, pricingUp) = (pricing[0], pricing[1]);

        var results = new List<IList<int>>();
        var items = new List<(int row, int column, int value)>() { (start[0], start[1], grid[start[0]][start[1]]) };
        grid[start[0]][start[1]] = -1;
        var itemsBuffer = new List<(int row, int column, int value)>();
        while (items.Count > 0)
        {
            foreach (var item in items
                .OrderBy(item => item.value)
                .ThenBy(item => item.row)
                .ThenBy(item => item.column))
            {
                itemsBuffer.Add(item);
                if (item.value >= pricingDown && item.value <= pricingUp)
                {
                    results.Add(new List<int> { item.row, item.column });
                    if (results.Count == k)
                        return results;
                }
            }

            items.Clear();
            items.AddRange(itemsBuffer.SelectMany(item => GetNeighbours(grid, item.row, item.column)));
            itemsBuffer.Clear();
        }
        return results;
    }

    private static IEnumerable<(int row, int column, int value)> GetNeighbours(int[][] grid, int row, int column)
    {
        if (row - 1 >= 0 && grid[row - 1][column] > 0)
        {
            var value = grid[row - 1][column];
            grid[row - 1][column] = -1;
            yield return (row - 1, column, value);
        }

        if (row + 1 < grid.Length && grid[row + 1][column] > 0)
        {
            var value = grid[row + 1][column];
            grid[row + 1][column] = -1;
            yield return (row + 1, column, value);
        }

        if (column - 1 >= 0 && grid[row][column - 1] > 0)
        {
            var value = grid[row][column - 1];
            grid[row][column - 1] = -1;
            yield return (row, column - 1, value);
        }

        if (column + 1 < grid[row].Length && grid[row][column + 1] > 0)
        {
            var value = grid[row][column + 1];
            grid[row][column + 1] = -1;
            yield return (row, column + 1, value);
        }
    }
}
