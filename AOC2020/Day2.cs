using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class Day2 : Solution<int>
    {
        string[] input;
        Regex rx = new Regex(@"(\d\d?)-(\d\d?)\s(\w):\s(\w+)", RegexOptions.Compiled);

        public Day2()
        {
            InputReader ir = new InputReader(2);
            input = ir.GetInput('\n');
        }

        public override int RunSilver()
        {
            int valid = 0;
            foreach(string line in input)
            {
                var groups = rx.Match(line).Groups;
                (int min, int max) = (int.Parse(groups[1].Value), int.Parse(groups[2].Value));
                int count = groups[4].Value.Trim().Count(c => c == groups[3].Value[0]);
                if (count >= min && count <= max) valid++;
            }
            return valid;
        }

        public override int RunGold()
        {
            int valid = 0;
            foreach (string line in input)
            {
                var groups = rx.Match(line).Groups;
                (int min, int max) = (int.Parse(groups[1].Value), int.Parse(groups[2].Value));
                char letter = groups[3].Value[0];
                if ((groups[4].Value[min - 1] == letter) ^ (groups[4].Value[max - 1] == letter)) valid++;
            }
            return valid;
        }
    }
}
