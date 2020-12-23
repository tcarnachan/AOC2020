using System.Linq;

namespace AOC2020
{
    public class Day6
    {
        string[] groups;

        public Day6()
        {
            InputReader ir = new InputReader(6);
            groups = ir.GetInput("\n\n");
        }

        public int RunSilver()
        {
            return groups.Select(g => g.Replace("\n", "").Distinct().Count()).Sum(); ;
        }

        public int RunGold()
        {
            var alphabet = Enumerable.Range('a', 26).Select(i => (char)i);
            return groups.Select(g => alphabet.Count(c => g.Split('\n').All(p => p.Contains(c)))).Sum();
        }
    }
}
