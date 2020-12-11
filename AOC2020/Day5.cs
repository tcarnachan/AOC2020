using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day5 : Solution
    {
        HashSet<int> seats = new HashSet<int>();

        public Day5()
        {
            InputReader ir = new InputReader(5);
            foreach(string pass in ir.GetInput('\n'))
                seats.Add(pass.Select(c => "FL".Contains(c) ? 0 : 1).Aggregate(0, (prod, x) => 2 * prod + x));
        }

        public override long RunSilver()
        {
            return seats.Max();
        }

        public override long RunGold()
        {
            for (int i = 1; i < seats.Max(); i++)
            {
                if (!seats.Contains(i) && seats.Contains(i - 1) && seats.Contains(i + 1))
                    return i;
            }
            return -1;
        }
    }
}
