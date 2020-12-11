using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class Day7 : Solution
    {
        Dictionary<string, Dictionary<string, int>> contains = new Dictionary<string, Dictionary<string, int>>();
        static string myBag = "shiny gold";
        static Regex keyRx = new Regex(@"(.*)\sbags?");
        static Regex valRx = new Regex(@"(\d)\s(.*)\sbags?.?");

        public Day7()
        {
            InputReader ir = new InputReader(7);
            foreach (string line in ir.GetInput('\n'))
            {
                string[] split = line.Split(new string[] { " contain " }, StringSplitOptions.None);
                Dictionary<string, int> dict = new Dictionary<string, int>();
                foreach (string v in split[1].Split(','))
                {
                    var groups = valRx.Match(v).Groups;
                    if (groups[2].Value != string.Empty)
                        dict[groups[2].Value] = int.Parse(groups[1].Value);
                }
                contains[keyRx.Match(split[0]).Groups[1].Value] = dict;
            }
        }

        public override long RunSilver()
        {
            Queue<string> queue = new Queue<string>(new string[] { myBag });
            HashSet<string> seen = new HashSet<string>() { myBag };
            while (queue.Count > 0)
            {
                string v = queue.Dequeue();
                var elems = contains.Where(kvp => kvp.Value.ContainsKey(v) && seen.Add(kvp.Key));
                elems.Select(e => e.Key).ToList().ForEach(queue.Enqueue);
            }
            return seen.Count - 1;
        }

        public override long RunGold()
        {
            Queue<string> queue = new Queue<string>(new string[] { myBag });
            int count = 0;
            while (queue.Count > 0)
            {
                string k = queue.Dequeue();
                foreach (var bags in contains[k])
                {
                    count += bags.Value;
                    for (int i = 0; i < bags.Value; i++)
                        queue.Enqueue(bags.Key);
                }
            }
            return count;
        }
    }
}
