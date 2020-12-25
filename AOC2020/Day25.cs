using System.Numerics;

namespace AOC2020
{
    public class Day25
    {
        int[] keys;

        public Day25()
        {
            InputReader ir = new InputReader(25);
            keys = ir.GetIntArray();
        }

        public long RunSilver()
        {
            long loop, t = 1;
            for (loop = 0; t != keys[0]; loop++) t = (t * 7) % 20201227;
            return (long)BigInteger.ModPow(keys[1], loop, 20201227);
        }

        public string RunGold() => "No puzzle";
    }
}