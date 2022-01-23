public class Solution
{
    public int[] RearrangeArray(int[] nums)
    {
        var positives = nums.Where(num => num > 0).ToArray();
        var negatives = nums.Where(num => num < 0).ToArray();
        for (int i = 0; i != nums.Length / 2; i++)
        {
            nums[i * 2] = positives[i];
            nums[i * 2 + 1] = negatives[i];
        }
        return nums;
    }
}
