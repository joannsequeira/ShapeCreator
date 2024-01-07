using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{





    public class IfCond : ICmd
    {
        private readonly Shape shape;

        public IfCond(Shape shape)
        {
            this.shape = shape;

        }

        public void Excecute(GroupCollection group)
        {

        }

        public void Excecute(GroupCollection group, string command)
        {
            string condition = group[1].Value;
            bool condRes = CondEval(condition);

            if (condRes)
            {
                InsideIf(group[2].Value);
            }
        }

        private bool CondEval(string condition)
        {
            var assignmentMatch = Regex.Match(condition, @"(\w+) = (\d+)");
            if (assignmentMatch.Success)
            {
                string variableName = assignmentMatch.Groups[1].Value;
                int variableValue = int.Parse(assignmentMatch.Groups[2].Value);

                shape.SetVar(variableName, variableValue);
                return true;
            }




            var match = Regex.Match(condition, @" (\w+)([<>]=?) (\d+)");  //Operators for the condition are assessed to retrive comparison value
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
                        return value < compVal; // For '<', consider a value just below the threshold
                    case ">":
                        return value > compVal; // For '>', consider a value just above the threshold

                    default:
                        throw new ArgumentException($"Unsupported comparison operator: {Operator}");
                }
            }
            throw new ArgumentException($"Unable to extract threshold from condition: {condition}");
        }

        public void InsideIf(string block)
        {

            string[] commands = block.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commands)
            {
                CommandParser.Parse(cmd.Trim(), shape.cmdLists.CList);
            }
        }
    }
}

