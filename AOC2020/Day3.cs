namespace AOC2020
{
    public class Day3 : Solution
    {
        string[] map;

        public Day3()
        {
            InputReader ir = new InputReader(3);
            map = ir.GetInput('\n');
        }

        public override long RunSilver()
        {
            return GetTrees(3, 1);
        }

        public override long RunGold()
        {
            return GetTrees(1, 1) * GetTrees(3, 1) * GetTrees(5, 1) * GetTrees(7, 1) * GetTrees(1, 2);
        }

        private long GetTrees(int dx, int dy)
        {
            long trees = 0;
            (int x, int y) pos = (0, 0);
            do
            {
                if (map[pos.y][pos.x % map[0].Length] == '#') trees++;
                pos = (pos.x + dx, pos.y + dy);
            } while (pos.y < map.Length);
            return trees;
        }
    }
}
