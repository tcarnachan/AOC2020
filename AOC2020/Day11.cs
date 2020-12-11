using System.Linq;

namespace AOC2020
{
    public class Day11 : Solution
    {
        char[][] seats, copy;

        public Day11()
        {
            InputReader ir = new InputReader(11);
            seats = ir.GetInput('\n').Select(line => line.ToCharArray()).ToArray();
        }

        public override long RunSilver() => GetOccupiedSeats(true, 4);

        public override long RunGold() => GetOccupiedSeats(false, 5);

        private int GetOccupiedSeats(bool checkFirst, int tolerance)
        {
            copy = seats; // Don't change the initial state
            while (true)
            {
                char[][] temp = ApplyRules(checkFirst, tolerance);
                bool unchanged = true;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!temp[i].SequenceEqual(copy[i]))
                    {
                        unchanged = false;
                        break;
                    }
                }
                if (unchanged) return temp.Select(r => r.Count(c => c == '#')).Sum();
                copy = temp;
            }
        }

        private char[][] ApplyRules(bool onlyAdj, int tolerance)
        {
            char[][] newSeats = new char[copy.Length][];
            for (int y = 0; y < copy.Length; y++)
            {
                newSeats[y] = new char[copy[0].Length];
                for (int x = 0; x < copy[0].Length; x++)
                {
                    int adj = 0;
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (y + dy < 0 || y + dy >= copy.Length) continue;
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if ((dx == 0 && dy == 0) || x + dx < 0 || x + dx >= copy[0].Length) continue;
                            adj += OccupiedSeatInDir(onlyAdj, y, x, dy, dx) ? 1 : 0;
                        }
                    }
                    if (copy[y][x] == '.') newSeats[y][x] = '.';
                    else if (copy[y][x] == 'L') newSeats[y][x] = adj == 0 ? '#' : 'L';
                    else if (copy[y][x] == '#') newSeats[y][x] = adj >= tolerance ? 'L' : '#';
                }
            }
            return newSeats;
        }

        private bool OccupiedSeatInDir(bool onlyAdj, int y, int x, int dy, int dx)
        {
            if (onlyAdj) return copy[y + dy][x + dx] == '#';

            for(int tx = x + dx, ty = y + dy;
                tx >= 0 && tx < copy[0].Length && ty >= 0 && ty < copy.Length;
                tx += dx, ty += dy)
            {
                if (copy[ty][tx] != '.')
                    return copy[ty][tx] == '#';
            }
            return false;
        }
    }
}