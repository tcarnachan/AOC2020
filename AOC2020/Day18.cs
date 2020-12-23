using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day18
    {
        string[][] expressions;
        public Day18()
        {
            InputReader ir = new InputReader(18);
            string[] t = ir.GetInput('\n');
            expressions = t.Select(s => s.Replace("(", "( ").Replace(")", " )").Split(' ')).ToArray();
        }

        public long RunSilver() => expressions.Select(t => EvaluateExpression(t, Calc1)).Sum();

        public long RunGold() => expressions.Select(t => EvaluateExpression(t, Calc2)).Sum();

        private long EvaluateExpression(string[] expression, Func<string[], long> Evaluate)
        {
            // Handle brackets
            if (expression.Contains("("))
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == "(")
                    {
                        int nestedBrackets = 1, j;
                        for (j = i + 1; nestedBrackets > 0; j++)
                        {
                            if (expression[j] == "(") nestedBrackets++;
                            else if (expression[j] == ")") nestedBrackets--;
                        }
                        string[] subexpression = new string[j - i - 2];
                        Array.Copy(expression, i + 1, subexpression, 0, subexpression.Length);
                        i = j - 1;
                        temp.Add(EvaluateExpression(subexpression, Evaluate).ToString());
                    }
                    else temp.Add(expression[i]);
                }
                expression = temp.ToArray();
            }
            // Calculate result
            return Evaluate(expression);
        }

        private long Calc1(string[] expression)
        {
            long res = 0;
            Func<long, long, long> op = (a, b) => a + b;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == "*") op = (a, b) => a * b;
                else if (expression[i] == "+") op = (a, b) => a + b;
                else res = op(res, long.Parse(expression[i]));
            }
            return res;
        }

        private long Calc2(string[] expression)
        {
            if (expression.Contains("*"))
            {
                int ix = Array.IndexOf(expression, "*");
                string[] lhs = new string[ix], rhs = new string[expression.Length - ix - 1];
                Array.Copy(expression, lhs, ix);
                Array.Copy(expression, ix + 1, rhs, 0, rhs.Length);
                return Calc2(lhs) * Calc2(rhs);
            }
            return Calc1(expression);
        }
    }
}