﻿using System.Collections.Generic;

namespace AOC2020
{
    public class Day1
    {
        int[] nums;
        HashSet<int> set;

        public Day1()
        {
            InputReader ir = new InputReader(1);
            nums = ir.GetIntArray();
            set = new HashSet<int>(nums);
        }

        public int RunSilver()
        {
            foreach(int n in nums) if (set.Contains(2020 - n)) return n * (2020 - n);
            return -1;
        }

        public int RunGold()
        {
            for(int i = 0; i < nums.Length; i++)
            {
                int ni = nums[i];
                for(int j = i + 1; j < nums.Length; j++)
                {
                    int nj = nums[j];
                    if (set.Contains(2020 - ni - nj))
                        return ni * nj * (2020 - ni - nj);
                }
            }
            return -1;
        }
    }
}
