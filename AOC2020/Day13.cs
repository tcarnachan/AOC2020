using System;

namespace AOC2020
{
    public class Day13 : Solution
    {
        string[] buses;
        int departTime;

        public Day13()
        {
            InputReader ir = new InputReader(13);
            string[] inp = ir.GetInput('\n', ',');
            departTime = int.Parse(inp[0]);

            buses = new string[inp.Length - 1];
            Array.Copy(inp, 1, buses, 0, buses.Length);
        }

        public override long RunSilver()
        {
            int busID = -1, time = int.MaxValue;
            foreach(string bus in buses)
            {
                int id, i;
                if (!int.TryParse(bus, out id)) continue;
                for (i = 0; i < departTime; i += id) ;
                if (i < time) (busID, time) = (id, i);
            }
            return busID * (time - departTime);
        }

        public override long RunGold()
        {
            long start = long.Parse(buses[0]), delta = start, bus;
            for(int i = 1; i < buses.Length; i++)
            {
                if (!long.TryParse(buses[i], out bus)) continue;
                for (; (start + i) % bus != 0; start += delta) ;
                delta *= bus;
            }
            return start;
        }
    }
}
