using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class Day19 : Solution<int>
    {
        Dictionary<string, string> rules = new Dictionary<string, string>();
        string[] messages;

        public Day19()
        {
            InputReader ir = new InputReader(19);
            string[] input = ir.GetInput("\n\n");

            foreach(string s in input[0].Split('\n'))
            {
                string[] t = s.Split(new string[] { ": " }, StringSplitOptions.None);
                rules[t[0]] = t[1];
            }

            messages = input[1].Split('\n');
        }

        public override int RunSilver()
        {
            Regex rx = new Regex($"^{GetRegex("0")}$");
            return messages.Count(m => rx.IsMatch(m));
        }

        private string GetRegex(string key)
        {
            string rule = rules[key];
            if (rule.Contains('"'))
                return $"{rule[1]}";
            else if (rule.Contains("|"))
            {
                string[] parts = rule.Split(new string[] { " | " }, StringSplitOptions.None);
                return $"({string.Join("|", parts.Select(part => string.Concat(part.Split(' ').Select(p => GetRegex(p)))))})";
            }
            return string.Concat(rule.Split(' ').Select(p => GetRegex(p)));
        }

        public override int RunGold()
        {
            // Hacky solution by just assuming recursion depth is small
            rules["8"] = string.Join(" | ", Enumerable.Range(1, 5).Select(i => string.Join(" ", Enumerable.Repeat("42", i))));
            rules["11"] = string.Join(" | ", Enumerable.Range(1, 5).Select(i =>
                string.Concat(Enumerable.Repeat("42 ", i)) + string.Join(" ", Enumerable.Repeat("31", i))
            ));
            Regex rx = new Regex($"^{GetRegex("0")}$");
            return messages.Count(m => rx.IsMatch(m));
        }
    }
}
