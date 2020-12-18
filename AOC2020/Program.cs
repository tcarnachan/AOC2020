using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Diagnostics;

namespace AOC2020
{
    class MainClass
    {
        public static void Main()
        {
            var solution = new Day18();

            Stopwatch sw = Stopwatch.StartNew();
            var res = solution.RunSilver();
            sw.Stop();
            Console.WriteLine($"Part 1: {res} | Executed in {sw.ElapsedMilliseconds}ms");

            sw = Stopwatch.StartNew();
            res = solution.RunGold();
            sw.Stop();
            Console.WriteLine($"Part 2: {res} | Executed in {sw.ElapsedMilliseconds}ms");
        }
    }

    public abstract class Solution<T>
    {
        public abstract T RunSilver();
        public abstract T RunGold();
    }

    class InputReader
    {
        string input;

        public InputReader(int day)
        {
            string filepath = $"day{day}.txt";
            if (File.Exists(filepath))
                input = File.ReadAllText(filepath);
            else
            {
                WebClient client = new WebClient();
                string id = File.ReadAllText("session_id.txt");
                client.Headers.Add(HttpRequestHeader.Cookie, $"session={id}");
                input = client.DownloadString($"https://adventofcode.com/2020/day/{day}/input").TrimEnd('\n');
                File.WriteAllText(filepath, input);
            }
        }

        public string GetInput()
        {
            return input;
        }

        public string[] GetInput(params string[] separators)
        {
            return input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] GetInput(params char[] separators)
        {
            return input.Split(separators);
        }

        public int[] GetIntArray()
        {
            return input.Split('\n').Select(int.Parse).ToArray();
        }

        public long[] GetLongArray()
        {
            return input.Split('\n').Select(long.Parse).ToArray();
        }
    }

    /*
     * Template
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class DayXX : Solution<long>
    {
        public DayXX()
        {
            InputReader ir = new InputReader(XX);
        }

        public override long RunSilver()
        {
            return -1;
        }

        public override long RunGold()
        {
            return -1;
        }
    }
}
     */
}
