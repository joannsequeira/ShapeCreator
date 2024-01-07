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

        public void Excecute(GroupCollection group, string command) 
        {
            string condition = group[1].Value;

            if(CondEval(condition))
            {
                shape.InsideIf(command);
            }
        }

        private bool CondEval(string condition)
        {
            int compVal = ExtractVal(condition);
            int values = shape.GetVar("Var");

            switch (condition)
            {
                case "Var == Threshold":
                    return values == compVal;
                case "Var < Threshold":
                    return values < compVal;
                case "Var > Threshold":
                    return values > compVal;

                default:
                    throw new ArgumentException($"Not supported");

            }
        }

        private int ExtractVal(string condition)
        {
            var match = Regex.Match(condition, @"([<>]=?) (\d+)");  //Operators for the condition are assessed to retrive comparison value
            if (match.Success)
            {
                string Operator = match.Groups[1].Value;
                int threshold = int.Parse(match.Groups[2].Value);

                switch (Operator)
                {
                    case "==":
                        return threshold;
                    case "<":
                        return threshold - 1; // For '<', consider a value just below the threshold
                    case ">":
                        return threshold + 1; // For '>', consider a value just above the threshold
                   
                    default:
                        throw new ArgumentException($"Unsupported comparison operator: {Operator}");
                }
            }
            throw new ArgumentException($"Unable to extract threshold from condition: {condition}");
        }

    }

    

}
