// https://leetcode.com/problems/maximum-good-people-based-on-statements/

var solution = new Solution();
var statements = new int[][] 
{ 
    new int[] { 2, 1, 2 }, 
    new int[] { 1, 2, 2 }, 
    new int[] { 2, 0, 2 } 
};
Console.WriteLine(solution.MaximumGood(statements));
