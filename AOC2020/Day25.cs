namespace AOC2020
{
    public class Day25
    {
        int[] keys;
        int subjNo = 7;

        public Day25()
        {
            InputReader ir = new InputReader(25);
            keys = ir.GetIntArray();
        }

        public long RunSilver()
        {
            long loop, t = 1;
            for (loop = 0; t != keys[0]; loop++) t = (t * subjNo) % 20201227;
            (subjNo, t) = (keys[1], 1);
            for (int i = 0; i < loop; i++) t = (t * subjNo) % 20201227;
            return t;
        }

        public string RunGold() => "No puzzle";
    }
}