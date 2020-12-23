using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day10
    {
        int[] adapters;

        public Day10()
        {
            InputReader ir = new InputReader(10);
            adapters = ir.GetIntArray();
            Array.Sort(adapters);
        }

        public long RunSilver()
        {
            int[] diff = new int[4];
            for (int i = 1; i < adapters.Length; i++) diff[adapters[i] - adapters[i - 1]]++;
            diff[adapters[0]]++;
            return diff[1] * (diff[3] + 1);
        }

        Dictionary<int, long> memo = new Dictionary<int, long>();

        public long RunGold()
        {
            memo[0] = 1;
            return CountArrangements(adapters.Last() + 3);
        }

        private long CountArrangements(int target)
        {
            if (!memo.ContainsKey(target))
            {
                long arr = 0;
                for(int i = 1; i <= 3; i++)
                {
                    if (adapters.Contains(target - i) || target - i == 0)
                        arr += CountArrangements(target - i);
                }
                memo[target] = arr;
            }
            return memo[target];
        }
    }
}
