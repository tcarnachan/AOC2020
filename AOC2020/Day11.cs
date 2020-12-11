using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

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

        public override long RunSilver()
        {
            return GetOccupiedSeats(true, 4);
        }

        public override long RunGold()
        {
            return GetOccupiedSeats(false, 5);
        }

        private int GetOccupiedSeats(bool checkFirst, int tolerance)
        {
            // Uncomment code for visualisation
            //int j = 0;
            copy = new char[seats.Length][]; // Store the initial state for part 2
            for (int i = 0; i < seats.Length; i++)
            {
                copy[i] = new char[seats[i].Length];
                Array.Copy(seats[i], copy[i], copy[i].Length);
            }
            while (true)
            {
                char[][] temp = ApplyRules(checkFirst, tolerance);
                bool unchanged = true;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!temp[i].SequenceEqual(seats[i]))
                    {
                        unchanged = false;
                        break;
                    }
                }
                if (unchanged)
                {
                    seats = copy; // Reset initial state
                    return temp.Select(r => r.Count(c => c == '#')).Sum();
                }
                seats = temp;

                /*if ((j++) % 2 == 0)
                {
                    Visualise();
                    Thread.Sleep(75);
                }*/
            }
        }

        private char[][] ApplyRules(bool onlyAdj, int tolerance)
        {
            char[][] newSeats = new char[seats.Length][];
            for (int y = 0; y < seats.Length; y++)
            {
                char[] row = new char[seats[0].Length];
                for (int x = 0; x < seats[0].Length; x++)
                {
                    int adj = 0;
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (y + dy < 0 || y + dy >= seats.Length)
                            continue;
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if ((dx == 0 && dy == 0) || x + dx < 0 || x + dx >= seats[0].Length)
                                continue;
                            adj += OccupiedSeatInDir(onlyAdj, y, x, dy, dx) ? 1 : 0;
                        }
                    }
                    if (seats[y][x] == '.') row[x] = '.';
                    else if (seats[y][x] == 'L') row[x] = adj == 0 ? '#' : 'L';
                    else if (seats[y][x] == '#') row[x] = adj >= tolerance ? 'L' : '#';
                }
                newSeats[y] = row;
            }
            return newSeats;
        }

        private bool OccupiedSeatInDir(bool onlyAdj, int y, int x, int dy, int dx)
        {
            if (onlyAdj)
                return seats[y + dy][x + dx] == '#';

            for(int tx = x + dx, ty = y + dy;
                tx >= 0 && tx < seats[0].Length && ty >= 0 && ty < seats.Length;
                tx += dx, ty += dy)
            {
                if (seats[ty][tx] == 'L')
                    return false;
                if (seats[ty][tx] == '#')
                    return true;
            }
            return false;
        }

        Dictionary<char, ConsoleColor> colours = new Dictionary<char, ConsoleColor>()
        {
            { '.', ConsoleColor.Black },
            { 'L', ConsoleColor.Red },
            { '#', ConsoleColor.Green }
        };

        public void Visualise()
        {
            Console.SetCursorPosition(0, 0);
            foreach(char[] row in seats)
            {
                foreach(char c in row)
                {
                    Console.BackgroundColor = colours[c];
                    Console.Write("  ");
                }
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            Console.ResetColor();
        }
    }
}