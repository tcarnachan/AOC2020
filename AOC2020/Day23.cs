using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day23
    {
        List<int> cups;

        public Day23()
        {
            cups = "362981754".Select(c => c - '0').ToList();
        }

        public long RunSilver()
        {
            int[] lookup = Simulate(9, 100);
            int num = lookup[1];
            while (lookup[num % 10] != 1) num = num * 10 + lookup[num % 10];
            return num;
        }

        public long RunGold()
        {
            cups.AddRange(Enumerable.Range(10, 999991));
            int[] lookup = Simulate(1000000, 10000000);
            return (long)lookup[1] * lookup[lookup[1]];
        }

        private int[] Simulate(int max, int iters)
        {
            int[] nextVal = new int[max + 1];
            for (int i = 0; i < cups.Count; i++) nextVal[cups[i]] = cups[(i + 1) % cups.Count];

            int current = cups[0];
            for (int i = 0; i < iters; i++)
            {
                int[] temp = new int[] { nextVal[current], nextVal[nextVal[current]], nextVal[nextVal[nextVal[current]]] };
                int destination = current;
                do
                {
                    destination--;
                    if (destination <= 0) destination = max;
                } while (temp.Contains(destination));

                int t = nextVal[destination];
                nextVal[destination] = temp[0];
                nextVal[current] = nextVal[temp[2]];
                nextVal[temp[2]] = t;

                current = nextVal[current];
            }
            return nextVal;
        }
    }
}
