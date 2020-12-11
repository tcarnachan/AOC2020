using System.Collections.Generic;

namespace AOC2020
{
    public class Day8 : Solution
    {
        string[] program;

        public Day8()
        {
            InputReader ir = new InputReader(8);
            program = ir.GetInput('\n');
        }

        public override long RunSilver()
        {
            int pc = 0, accumulator = 0;
            HashSet<int> prev = new HashSet<int>();
            while (Step(ref pc, ref accumulator, prev) == ExecutionState.Normal) ;
            return accumulator;
        }

        public override long RunGold()
        {
            List<(string, int)> ops = new List<(string, int)>();
            for (int i = 0; i < program.Length; i++)
            {
                if (program[i].StartsWith("nop")) ops.Add(("jmp", i));
                else if (program[i].StartsWith("jmp")) ops.Add(("nop", i));
            }

            foreach ((string instr, int ix) op in ops)
            {
                string t = program[op.ix];
                program[op.ix] = $"{op.instr} {program[op.ix].Split()[1]}";
                int pc = 0, accumulator = 0;
                HashSet<int> prev = new HashSet<int>();
                ExecutionState execVal = ExecutionState.Normal;
                while (execVal == ExecutionState.Normal)
                    execVal = Step(ref pc, ref accumulator, prev);
                if (execVal == ExecutionState.Halted)
                    return accumulator;
                program[op.ix] = t;
            }
            return -1;
        }

        private ExecutionState Step(ref int pc, ref int accumulator, HashSet<int> prevPCs)
        {
            if (!prevPCs.Add(pc))
                return ExecutionState.Looping;
            if (pc == program.Length)
                return ExecutionState.Halted;

            string[] data = program[pc++].Split();
            (string instruction, int operand) = (data[0], int.Parse(data[1]));

            switch (instruction)
            {
                case "nop":
                    break;
                case "jmp":
                    pc += operand - 1;
                    break;
                case "acc":
                    accumulator += operand;
                    break;
            }

            return ExecutionState.Normal;
        }

        enum ExecutionState { Normal, Looping, Halted }
    }
}
