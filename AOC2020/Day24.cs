using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day24
    {
        string[] instructions;

        public Day24()
        {
            InputReader ir = new InputReader(24);
            instructions = ir.GetInput('\n');
        }

        Dictionary<(int x, int y, int z), bool> tiles = new Dictionary<(int, int, int), bool>();

        (int x, int y, int z)[] adj = new (int x, int y, int z)[]
        {
            (1, -1, 0), (-1, 1, 0), (1, 0, -1), (0, -1, 1), (0, 1, -1), (-1, 0, 1)
        };
        string[] neighbours = new string[] { "e", "w", "ne", "se", "nw", "sw" };

        public long RunSilver()
        {
            foreach (string instr in instructions)
            {
                (int x, int y, int z) curr = (0, 0, 0);
                for (int i = 0; i < instr.Length; i++)
                {
                    string dir = instr[i].ToString();
                    if ("ns".Contains(dir))
                    {
                        dir += instr[i + 1];
                        i++;
                    }
                    var delta = adj[Array.IndexOf(neighbours, dir)];
                    curr = (curr.x + delta.x, curr.y + delta.y, curr.z + delta.z);
                }
                tiles[curr] = !(tiles.ContainsKey(curr) && tiles[curr]);
            }

            return tiles.Count(kvp => kvp.Value);
        }

        public long RunGold()
        {
            for (int day = 0; day < 100; day++)
            {
                // Add adjacent
                foreach (var coord in tiles.Keys.Where(k => tiles[k]).ToArray())
                {
                    foreach (var delta in adj)
                    {
                        var t = (coord.x + delta.x, coord.y + delta.y, coord.z + delta.z);
                        if (!tiles.ContainsKey(t)) tiles[t] = false;
                    }
                }
                // Flip tiles
                List<(int, int, int)> toFlip = new List<(int, int, int)>();
                foreach (var coord in tiles.Keys)
                {
                    int adjCount = 0;
                    foreach (var delta in adj)
                    {
                        var t = (coord.x + delta.x, coord.y + delta.y, coord.z + delta.z);
                        if (tiles.ContainsKey(t) && tiles[t]) adjCount++;
                    }
                    if ((tiles[coord] && (adjCount == 0 || adjCount > 2)) || (!tiles[coord] && adjCount == 2))
                        toFlip.Add(coord);
                }
                foreach (var coord in toFlip) tiles[coord] = !tiles[coord];
            }

            return tiles.Count(kvp => kvp.Value);
        }
    }
}