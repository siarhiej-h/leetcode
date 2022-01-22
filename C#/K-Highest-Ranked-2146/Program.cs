// https://leetcode.com/problems/k-highest-ranked-items-within-a-price-range/
var solution = new Solution();
var grid = new int[][] { new int[] { 0, 2, 0 } };
var pricing = new int[] { 2, 2 };
var start = new int[] { 0, 1 };
var k = 1;

var ranks = solution.HighestRankedKItems(grid, pricing, start, k);
Console.WriteLine($"[{string.Join(",", ranks.Select(ri => $"[{string.Join(",", ri) }]"))}]");
