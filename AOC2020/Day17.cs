using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day17
    {
        Dictionary<(int x, int y, int z, int w), bool> cubes = new Dictionary<(int, int, int, int), bool>();

        public Day17()
        {
            LoadCubes();
        }

        private void LoadCubes()
        {
            cubes.Clear();
            InputReader ir = new InputReader(17);
            string[] lines = ir.GetInput('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                    cubes[(i, j, 0, 0)] = line[j] == '#';
            }
        }

        public int RunSilver()
        {
            for (int i = 0; i < 6; i++)
                SimulateCycle(false);
            return cubes.Values.Count(b => b);
        }

        private void SimulateCycle(bool part2)
        {
            // Find adjacent cubes which are not being kept track of
            List<(int, int, int, int)> toAdd = new List<(int, int, int, int)>();
            foreach(var pos in cubes.Keys)
            {
                for(int i = -1; i <= 1; i++)
                {
                    for(int j = -1; j <= 1; j++)
                    {
                        for(int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                if (!part2 && l != 0) continue;
                                if (!cubes.ContainsKey((pos.x + i, pos.y + j, pos.z + k, pos.w + l)))
                                    toAdd.Add((pos.x + i, pos.y + j, pos.z + k, pos.w + l));
                            }
                        }
                    }
                }
            }
            foreach (var pos in toAdd)
                cubes[pos] = false;
            // Find the cubes that need to change
            List<(int, int, int, int)> toChange = new List<(int, int, int, int)>();
            foreach(var pos in cubes.Keys)
            {
                int neighbours = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                if (!part2 && l != 0) continue;
                                if (!(i == 0 && j == 0 && k == 0 && l == 0) &&
                                cubes.ContainsKey((pos.x + i, pos.y + j, pos.z + k, pos.w + l)) &&
                                cubes[(pos.x + i, pos.y + j, pos.z + k, pos.w + l)]
                                )
                                    neighbours++;
                            }
                        }
                    }
                }
                if ((cubes[pos] && !new int[] { 2, 3 }.Contains(neighbours)) ||
                    (!cubes[pos] && neighbours == 3))
                    toChange.Add(pos);
            }
            // Update cubes
            foreach (var pos in toChange)
                cubes[pos] = !cubes[pos];
        }

        public int RunGold()
        {
            LoadCubes();
            for (int i = 0; i < 6; i++)
                SimulateCycle(true);
            return cubes.Values.Count(b => b);
        }
    }
}