using System.Linq;

namespace AOC2020
{
    public class Day9 : Solution<long>
    {
        long[] nums;
        int preamble = 25;

        public Day9()
        {
            InputReader ir = new InputReader(9);
            nums = ir.GetLongArray();
        }

        public override long RunSilver()
        {
            for(int nextVal = preamble; nextVal < nums.Length; nextVal++)
            {
                bool validNum = false;
                for(int i = 0; i < preamble && !validNum; i++)
                {
                    long target = nums[nextVal] - nums[nextVal - i - 1];
                    for (int j = i + 1; j < preamble && !validNum; j++)
                        validNum = (nums[nextVal - j - 1] == target);
                }
                if (!validNum)
                    return nums[nextVal];
            }
            // Unreachable code to keep the compiler happy
            return -1;
        }

        public override long RunGold()
        {
            long target = RunSilver(), runningSum = 0;
            int start = 0, end;
            for (end = 0; runningSum != target; end++)
            {
                runningSum += nums[end];
                while (runningSum > target)
                    runningSum -= nums[start++];
            }
            var range = nums.Skip(start).Take(end - start);
            return range.Min() + range.Max();
        }
    }
}
