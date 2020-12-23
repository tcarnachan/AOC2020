using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    // C# can't compare arrays properly
    class ArrayComparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[] x, string[] y) => x != null && y != null && x.SequenceEqual(y);
        public int GetHashCode(string[] obj) => obj.Select(s => s.GetHashCode()).Aggregate((a, b) => a * b);
    }

    class Tile
    {
        public string[] data;
        public readonly int number;

        private List<string[]> transformations = new List<string[]>();
        public readonly int count;

        public Tile(string[] data, int number)
        {
            this.data = data;
            this.number = number;

            // Generate all possible transformations of this tile at the start
            HashSet<string[]> seen = new HashSet<string[]>(new ArrayComparer());
            Queue<string[]> queue = new Queue<string[]>();
            seen.Add(data);
            queue.Enqueue(data);
            transformations.Add(data);
            while (queue.Count > 0)
            {
                string[] tile = queue.Dequeue();
                string[] temp = tile.Reverse().ToArray();
                if (seen.Add(temp))
                {
                    queue.Enqueue(temp);
                    transformations.Add(temp);
                }
                string[] temp2 = Rotate(tile);
                if (seen.Add(temp2))
                {
                    queue.Enqueue(temp2);
                    transformations.Add(temp2);
                }
            }
            count = transformations.Count;
        }

        public void SetActive(int i)
        {
            data = transformations[i];
        }

        public string Top() => data[0];
        public string Bottom() => data[data.Length - 1];
        public string Left() => string.Concat(data.Select(r => r[0]));
        public string Right() => string.Concat(data.Select(r => r.Last()));

        private string[] Rotate(string[] data)
        {
            char[][] res = new char[data.Length][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new char[res.Length];
                for (int j = 0; j < res.Length; j++)
                    res[i][j] = data[j][res.Length - i - 1];
            }
            return res.Select(r => string.Concat(r)).ToArray();
        }
    }

    public class Day20
    {
        List<Tile> tiles = new List<Tile>();
        Tile[,] image;

        public Day20()
        {
            InputReader ir = new InputReader(20);
            foreach (string tile in ir.GetInput("\n\n"))
            {
                string[] tileData = tile.Split('\n');
                tiles.Add(new Tile(
                    tileData.Skip(1).Take(tileData.Length - 1).ToArray(),
                    int.Parse(tileData[0].TrimStart("Tile ".ToCharArray()).TrimEnd(':'))
                ));
            }
        }

        public long RunSilver()
        {
            int dim = (int)Math.Sqrt(tiles.Count);
            image = new Tile[dim, dim];
            bool[] used = new bool[tiles.Count];
            FindImage(0, dim, used);
            return (long)image[0, 0].number * image[0, dim - 1].number * image[dim - 1, 0].number * image[dim - 1, dim - 1].number;
        }

        private bool FindImage(int ix, int dim, bool[] used)
        {
            if (used.All(b => b))
                return true;

            for (int tile = 0; tile < tiles.Count; tile++)
            {
                if (used[tile]) continue;
                used[tile] = true;
                for (int i = 0; i < tiles[tile].count; i++)
                {
                    tiles[tile].SetActive(i);
                    image[ix / dim, ix % dim] = tiles[tile];
                    if (Valid(ix / dim, ix % dim) && FindImage(ix + 1, dim, used))
                        return true;
                }
                used[tile] = false;
            }

            image[ix / dim, ix % dim] = null;
            return false;
        }

        private bool Valid(int r, int c)
        {
            return !(
                (r > 0 && image[r - 1, c] != null && image[r - 1, c].Bottom() != image[r, c].Top()) // Check top
                || (c > 0 && image[r, c - 1] != null && image[r, c - 1].Right() != image[r, c].Left()) // Check left
            );
        }

        public long RunGold()
        {
            List<string> img = new List<string>();
            int dim = image[0, 0].data.Length - 2;
            for(int r = 0; r < image.GetLength(0); r++)
            {
                for (int line = 1; line < dim + 1; line++)
                {
                    List<char> s = new List<char>();
                    for (int c = 0; c < image.GetLength(1); c++)
                        s.AddRange(image[r, c].data[line].Skip(1).Take(dim));
                    img.Add(string.Concat(s));
                }
            }

            Tile t = new Tile(img.ToArray(), -1);
            string[] seaMonster = new string[]
            {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };

            bool foundSeaMonsters = false;
            int monsterCount = 0;
            dim *= image.GetLength(0);
            for (int i = 0; i < t.count; i++)
            {
                t.SetActive(i);
                int count = 0;
                for (int startIx = 0; startIx < dim * dim; startIx++)
                {
                    int r = startIx / dim, c = startIx % dim;
                    if (r + seaMonster.Length >= dim || c + seaMonster[0].Length >= dim) continue;
                    foundSeaMonsters = true;
                    for(int dr = 0; dr < seaMonster.Length && foundSeaMonsters; dr++)
                    {
                        for(int dc = 0; dc < seaMonster[0].Length && foundSeaMonsters; dc++)
                        {
                            if (seaMonster[dr][dc] == '#')
                                foundSeaMonsters = t.data[r + dr][c + dc] == '#';
                        }
                    }
                    if (foundSeaMonsters) count++;
                }
                if (count > monsterCount) monsterCount = count;
            }
            return t.data.Sum(s => s.Count(c => c =='#')) - seaMonster.Sum(r => r.Count(c => c == '#')) * monsterCount;
        }
    }
}