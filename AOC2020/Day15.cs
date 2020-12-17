using System.Linq;

namespace AOC2020
{
    public class Day15 : Solution<int>
    {
        int[] lastSaid = new int[30000000]; // Turns out to be roughly 5x faster than using a dictionary
        int currTurn = 1, prevNum, next;

        public Day15()
        {
            int[] input = "8,11,0,19,1,2".Split(',').Select(int.Parse).ToArray();
            for (int i = 0; i < input.Length - 1; i++)
                lastSaid[input[i]] = currTurn++;
            prevNum = input.Last();
        }

        public override int RunSilver() => PlayGame(2020);

        public override int RunGold() => PlayGame(30000000);

        private int PlayGame(int maxTurns)
        {
            for (; currTurn < maxTurns; currTurn++)
            {
                if (lastSaid[prevNum] != 0) next = currTurn - lastSaid[prevNum];
                else next = 0;
                lastSaid[prevNum] = currTurn;
                prevNum = next;
            }
            return prevNum;
        }
    }
}