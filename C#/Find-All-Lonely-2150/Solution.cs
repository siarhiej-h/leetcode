public class Solution
{
    public IList<int> FindLonely(int[] nums)
    {
        var dict = nums
            .GroupBy(num => num)
            .ToDictionary(group => group.Key, group => group.Count());
        return nums
            .Where(num => dict[num] == 1 && !dict.ContainsKey(num + 1) && !dict.ContainsKey(num - 1))
            .ToList();
    }
}