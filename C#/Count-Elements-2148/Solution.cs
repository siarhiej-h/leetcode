public class Solution
{
    public int CountElements(int[] nums)
    {
        Array.Sort(nums);
        return nums.Count(num => num > nums.First() && num < nums.Last());
    }
}
