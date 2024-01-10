using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public class IfCond : ICmd
    {
        private readonly string condition;
        private readonly CmdLists cmdLists;
        private readonly Shape shape;

        public IfCond(string condition, CmdLists cmdLists, Shape shape)
        {
            this.condition = condition;
            this.cmdLists = cmdLists;
            this.shape = shape;
        }

        public void Excecute(GroupCollection groups)
        {
            int threshold = ExtractThreshold(condition);

            // Extract the variable value from the dictionary
            int varValue = 0;
            if (shape.vars.TryGetValue("Var", out var existingValue))
            {
                varValue = existingValue;
            }

            // Check the condition
            bool conditionMet = CondEval(condition);

            // Execute or skip the commands based on the condition
            if (conditionMet)
            {
                InsideIf(condition);
            }
        }

        private bool CondEval(string condition)
        {
            var match = Regex.Match(condition, @" (\w+)\s*([<>]=?)\s*(\d+)");
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                string Operator = match.Groups[2].Value;

                if (int.TryParse(match.Groups[3].Value, out int compVal))
                {
                    int value = shape.GetVar(name);

                    switch (Operator)
                    {
                        case "==":
                            return value == compVal;
                        case "<":
                            return value < compVal;
                        case ">":
                            return value > compVal;
                        default:
                            throw new ArgumentException($"Unsupported comparison operator: {Operator}");
                    }
                }
            }

            throw new ArgumentException($"Unable to evaluate condition: {condition}");
        }

        private void InsideIf(string block)
        {
            string[] commands = block.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var cmd in commands)
            {
                CommandParser.Parse(cmd.Trim(), cmdLists.CList);
            }
        }

        private int ExtractThreshold(string condition)
        {
            var match = Regex.Match(condition, @"Var\s*([><=]+)\s*(\d+)");
            if (match.Success)
            {
                string comparisonOperator = match.Groups[1].Value;
                int threshold = int.Parse(match.Groups[2].Value);

                // Perform the appropriate comparison based on the operator
                switch (comparisonOperator)
                {
                    case "==": return threshold;
                    case ">": return threshold + 1;
                    case "<": return threshold - 1;
                    default:
                        throw new ArgumentException($"Unsupported comparison operator: {comparisonOperator}");
                }
            }

            Console.WriteLine($"Unable to extract threshold from condition: {condition}");
            throw new ArgumentException($"Unable to extract threshold from condition: {condition}");
        }

    }
}
