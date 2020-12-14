using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public class Day14 : Solution<ulong>
    {
        string mask;
        Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();
        string[] program;
        Regex memRx = new Regex(@"mem\[(\d*)\]");

        public Day14()
        {
            InputReader ir = new InputReader(14);
            program = ir.GetInput('\n');
        }

        public override ulong RunSilver()
        {
            foreach(string line in program)
            {
                string[] instr = line.Split(new string[] { " = " }, StringSplitOptions.None);
                if (instr[0] == "mask")
                    mask = instr[1];
                else
                {
                    ulong addr = ulong.Parse(memRx.Match(instr[0]).Groups[1].Value);
                    ulong val = ulong.Parse(instr[1]);
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[mask.Length - i - 1] == '1') val |= (ulong)1 << i;
                        else if (mask[mask.Length - i - 1] == '0') val &= ~((ulong)1 << i);
                    }
                    mem[addr] = val;
                }
            }
            return mem.Values.Aggregate((a, b) => a + b);
        }

        public override ulong RunGold()
        {
            mem.Clear();
            foreach (string line in program)
            {
                string[] instr = line.Split(new string[] { " = " }, StringSplitOptions.None);
                if (instr[0] == "mask")
                    mask = instr[1];
                else
                {
                    ulong addr = ulong.Parse(memRx.Match(instr[0]).Groups[1].Value);
                    ulong val = ulong.Parse(instr[1]);
                    List<ulong> values = new List<ulong>();
                    GetPossibleValues(addr, 0, values);
                    foreach (ulong a in values) mem[a] = val;
                }
            }
            return mem.Values.Aggregate((a, b) => a + b);
        }

        private void GetPossibleValues(ulong curr, int ix, List<ulong> values)
        {
            if (ix == mask.Length) values.Add(curr);
            else
            {
                char m = mask[mask.Length - ix - 1];
                if (m == '0') GetPossibleValues(curr, ix + 1, values);
                if (m == 'X') GetPossibleValues(curr & ~((ulong)1 << ix), ix + 1, values);
                if ("1X".Contains(m)) GetPossibleValues(curr | ((ulong)1 << ix), ix + 1, values);
            }
        }
    }
}