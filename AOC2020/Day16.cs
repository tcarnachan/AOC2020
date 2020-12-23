using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class Day16
    {
        Regex fieldRx = new Regex(@"(.*):\s(\d*)-(\d*)\sor\s(\d*)-(\d*)");

        List<(int, int)> validRanges = new List<(int, int)>();
        List<int[]> tickets = new List<int[]>();
        List<string> fieldNames = new List<string>();
        int[] myTicket;

        public Day16()
        {
            InputReader ir = new InputReader(16);
            string[] input = ir.GetInput("\n\n");

            foreach (string line in input[0].Split('\n'))
            {
                var groups = fieldRx.Match(line).Groups;
                fieldNames.Add(groups[1].Value);
                validRanges.Add((int.Parse(groups[2].Value), int.Parse(groups[3].Value)));
                validRanges.Add((int.Parse(groups[4].Value), int.Parse(groups[5].Value)));
            }

            myTicket = input[1].Split('\n')[1].Split(',').Select(int.Parse).ToArray();

            string[] ticketStrs = input[2].Split('\n');
            for (int i = 1; i < ticketStrs.Length; i++)
                tickets.Add(ticketStrs[i].Split(',').Select(int.Parse).ToArray());
        }

        public long RunSilver()
        {
            int invalidCount = 0;
            for(int i = 0; i < tickets.Count; i++)
            {
                foreach (int field in tickets[i])
                {
                    foreach(var range in validRanges)
                    {
                        if (range.Item1 <= field && field <= range.Item2)
                            goto next;
                    }
                    invalidCount += field;
                    tickets.RemoveAt(i--);
                    break;
                    next:;
                }
            }
            return invalidCount;
        }

        public long RunGold()
        {
            List<int>[] validFields = new List<int>[fieldNames.Count];
            for(int i = 0; i < fieldNames.Count; i++)
            {
                List<int> possFields = new List<int>();
                (int, int) range1 = validRanges[2 * i], range2 = validRanges[2 * i + 1];
                for(int testField = 0; testField < fieldNames.Count; testField++)
                {
                    foreach(int[] ticket in tickets)
                    {
                        if (!(range1.Item1 <= ticket[testField] && ticket[testField] <= range1.Item2) &&
                            !(range2.Item1 <= ticket[testField] && ticket[testField] <= range2.Item2))
                            goto next;
                    }
                    possFields.Add(testField);
                    next:;
                }
                validFields[i] = possFields;
            }

            List<int> remaining = Enumerable.Range(0, validFields.Length).ToList();
            while(remaining.Count > 0)
            {
                var known = remaining.Where(i => validFields[i].Count == 1).ToArray();
                foreach(int ix in known)
                {
                    remaining.Remove(ix);
                    int target = validFields[ix][0];
                    foreach(int i in remaining)
                    {
                        if (validFields[i].Contains(target))
                            validFields[i].Remove(target);
                    }
                }
            }

            long prod = 1;
            for(int i = 0; i < fieldNames.Count; i++)
            {
                if (fieldNames[i].Contains("departure"))
                    prod *= myTicket[validFields[i][0]];
            }
            return prod;
        }
    }
}