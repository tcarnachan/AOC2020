using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day22
    {
        int[] start1, start2;

        public Day22()
        {
            InputReader ir = new InputReader(22);
            string[] input = ir.GetInput("\n\n");
            start1 = input[0].Split('\n').Skip(1).Take(input[0].Length - 1).Select(int.Parse).ToArray();
            start2 = input[1].Split('\n').Skip(1).Take(input[1].Length - 1).Select(int.Parse).ToArray();
        }

        public long RunSilver()
        {
            Queue<int> deck1 = new Queue<int>(start1), deck2 = new Queue<int>(start2);
            while(deck1.Count > 0 && deck2.Count > 0)
            {
                int num1 = deck1.Dequeue(), num2 = deck2.Dequeue();
                if(num1 > num2)
                {
                    deck1.Enqueue(num1);
                    deck1.Enqueue(num2);
                }
                else
                {
                    deck2.Enqueue(num2);
                    deck2.Enqueue(num1);
                }
            }
            long score = 0;
            while (deck1.Count > 0) score += deck1.Count * deck1.Dequeue();
            while (deck2.Count > 0) score += deck2.Count * deck2.Dequeue();
            return score;
        }

        HashSet<string> prevRounds = new HashSet<string>();
        int winner = -1, nextGame = 1;

        public long RunGold()
        {
            Queue<int> deck1 = new Queue<int>(start1), deck2 = new Queue<int>(start2);
            RecursiveCombat(deck1, deck2, nextGame);
            long score = 0;
            while (deck1.Count > 0 && winner == 1) score += deck1.Count * deck1.Dequeue();
            while (deck2.Count > 0 && winner == 2) score += deck2.Count * deck2.Dequeue();
            return score;
        }

        private void RecursiveCombat(Queue<int> deck1, Queue<int> deck2, int game)
        {
            if (!prevRounds.Add($"{game},{string.Concat(deck1)},{string.Concat(deck2)}")) winner = 1;
            else
            {
                int num1 = deck1.Dequeue(), num2 = deck2.Dequeue();
                if (deck1.Count >= num1 && deck2.Count >= num2)
                {
                    Queue<int> temp1 = new Queue<int>(deck1.Take(num1));
                    Queue<int> temp2 = new Queue<int>(deck2.Take(num2));
                    RecursiveCombat(temp1, temp2, ++nextGame);
                }
                else winner = num1 > num2 ? 1 : 2;

                if (winner == 1)
                {
                    deck1.Enqueue(num1);
                    deck1.Enqueue(num2);
                }
                else
                {
                    deck2.Enqueue(num2);
                    deck2.Enqueue(num1);
                }

                if (deck1.Count == 0) winner = 2;
                else if (deck2.Count == 0) winner = 1;
                else RecursiveCombat(deck1, deck2, game);
            }
        }
    }
}
