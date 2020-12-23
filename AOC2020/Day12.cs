using System;

namespace AOC2020
{
    public class Day12
    {
        string[] instructions;

        public Day12()
        {
            InputReader ir = new InputReader(12);
            instructions = ir.GetInput('\n');
        }

        public int RunSilver()
        {
            (int x, int y) pos = (0, 0), dir = (1, 0);

            foreach(string instr in instructions)
            {
                (char action, int val) = (instr[0], int.Parse(instr.Substring(1)));
                if (action == 'N') pos = (pos.x, pos.y + val);
                else if (action == 'E') pos = (pos.x + val, pos.y);
                else if (action == 'S') pos = (pos.x, pos.y - val);
                else if (action == 'W') pos = (pos.x - val, pos.y);
                else if (action == 'L') dir = Rotate(dir.x, dir.y, val);
                else if (action == 'R') dir = Rotate(dir.x, dir.y, -val);
                else if (action == 'F') pos = (pos.x + dir.x * val, pos.y + dir.y * val);
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }

        private const double D2R = Math.PI / 180.0;
        private (int, int) Rotate(int x, int y, int val)
        {
            int sin = (int)Math.Sin(val * D2R), cos = (int)Math.Cos(val * D2R);
            return (x * cos - y * sin, y * cos + x * sin);
        }

        public int RunGold()
        {
            (int x, int y) ship = (0, 0), wpt = (10, 1);

            foreach (string instr in instructions)
            {
                (char action, int val) = (instr[0], int.Parse(instr.Substring(1)));
                if (action == 'N') wpt = (wpt.x, wpt.y + val);
                else if (action == 'E') wpt = (wpt.x + val, wpt.y);
                else if (action == 'S') wpt = (wpt.x, wpt.y - val);
                else if (action == 'W') wpt = (wpt.x - val, wpt.y);
                else if (action == 'L') wpt = Rotate(wpt.x, wpt.y, val);
                else if (action == 'R') wpt = Rotate(wpt.x, wpt.y, -val);
                else if (action == 'F') ship = (ship.x + wpt.x * val, ship.y + wpt.y * val);
            }

            return Math.Abs(ship.x) + Math.Abs(ship.y);
        }
    }
}