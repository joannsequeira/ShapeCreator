using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ShapeCreator
{

    public class CmdLists
    {
        public List<CommandEntry> CList { get; internal set; } //stores list of commands
        public Dictionary<string, int> variables = new Dictionary<string, int>(); //store variable names and values

        public CmdLists(Shape shape) //constructor used for command intialisation
        {
            CList = new List<CommandEntry>
            {
                new CommandEntry { CmdRg = @"clearsc", Command = new ClearScreen(shape)  },  //Clear Screen
                new CommandEntry { CmdRg = @"drawRect (\d+) (\d+)", Command = new RectClass(shape)  },  //Draw Rectangle
                new CommandEntry { CmdRg = @"colorin (true|false)", Command = new FillShape(shape)} ,  //Fill Shape
                new CommandEntry { CmdRg = @"resetPos", Command = new ResetPos(shape)},  //Reset Pointer
                new CommandEntry { CmdRg = @"penChange (\w+)", Command = new PenChange(shape)}, //Change color of Pen
                new CommandEntry { CmdRg = @"brushChange (\w+)", Command = new BrushChange(shape)}, //Change color of Brush
                new CommandEntry { CmdRg = @"drawCirc (\d+)", Command = new CircClass(shape)},  //Draw Circle
                new CommandEntry { CmdRg = @"drawTo (\d+) (\d+)", Command = new DrawTo(shape)}, //Draw Line
                new CommandEntry { CmdRg = @"moveTo (\d+) (\d+)", Command = new PenPos(shape)}, //Move pointer
                new CommandEntry { CmdRg = @"drawTri (\d+) (\d+) (\d+)", Command = new TriClass(shape)} //Draw Triangle

            };

        }

        public void Parse(string com)
        {
            string[] comLines = com.Split('\n'); //split commands based on next line
            bool inIfBlock = false;

            foreach (string line in comLines)
            {
                if(line.Contains("="))
                {
                    string[] parts = line.Split('='); //check line for "=" and split the line to parts
                    if(parts.Length == 2)
                    {
                        string varName= parts[0].Trim(); //store first part as var name
                        int varValue;

                        string opVal = parts[1].Trim();
                        int result = Op(opVal);
                        
                        /* if (!int.TryParse(parts[1].Trim(), out varValue))
                        {
                            throw new ArgumentException("Invalid value provided");
                        } */
                        VariableHandler(varName, result); 
                    }
                    else
                    {
                        throw new ArgumentException("Format for variable is wrong");
                    }
                }
                else if(line.StartsWith("if") && !inIfBlock)
                {
                    string condif = line.Substring(3).Trim();
                    if(!ConditionChecker(condif))
                    {
                        inIfBlock = true;
                    }
                    else if(line.StartsWith("endif") && inIfBlock)
                    {
                        inIfBlock = false;  //if statement logic call
                    }
                }
                else if (!inIfBlock)
                {
                    ExcecuteCom(line.Trim());
                }
            }
            //CommandParser.Parse(com, CList);  //parse coammand using the specified list
        }
        
        private void VariableHandler(string varName, int varValue)
        {
            variables[varName] = varValue; //store value with corresponding varname
        }

        private void ExcecuteCom(string com) {

            string[] parts = com.Split(' '); //split command into parts
            for (int i = 0; i< parts.Length; i++) //loop to check command parts
            {
                if (!int.TryParse(parts[i], out _)) //not a number
                {
                    if (variables.ContainsKey(parts[i])) //check if variable
                    {
                        parts[i] = variables[parts[i]].ToString(); //reaplce vName with value
                    }
                }
            }
            string newCom = string.Join(" ", parts); //join the command and replaced value
            Console.WriteLine("Check Com:" +  newCom);
            CommandParser.Parse(newCom, CList);
        }

        private string getVar(string stmt) //get val from dictionary or return input string com
        {
            if (variables.ContainsKey(stmt))
            {
                return variables[stmt].ToString(); 
            }
            return stmt;  //if not found
        }

        private int Op(string opVal)  //trying for var int. like c = c + 10 etc
        {

            int result;
            if (opVal.Contains("+"))  //check for operator symbol
            {
                string[] opSplit = opVal.Split('+');
                if (opSplit.Length != 2)
                {
                    throw new ArgumentException("Invalid op value given");
                }


                var op1 = getVar(opSplit[0].Trim());  //split operands into op1
                var op2 = getVar(opSplit[1].Trim());  //and operant op2

                int op1Int;
                int op2Int;
                if (!int.TryParse(op1, out op1Int))
                {
                    throw new ArgumentException("Invalid op1 value provided");
                }

                if (!int.TryParse(op2, out op2Int))
                {
                    throw new ArgumentException("Invalid op2 value provided");
                }
                result = op1Int + op2Int;   //result after performing arithmetic operation
            }
            else if (opVal.Contains("-"))  //subtraction
            {
                string[] opSplit = opVal.Split('-');
                if (opSplit.Length != 2)
                {
                    throw new ArgumentException("Invalid op value given");
                }
                var op1 = getVar(opSplit[0].Trim());
                var op2 = getVar(opSplit[1].Trim());

                int op1Int;
                int op2Int;
                if (!int.TryParse(op1, out op1Int))
                {
                    throw new ArgumentException("Invalid op1 value provided");
                }

                if (!int.TryParse(op2, out op2Int))
                {
                    throw new ArgumentException("Invalid op2 value provided");
                }
                result = op1Int - op2Int;
            }
            else if (opVal.Contains("*"))  //multiplication
            {
                string[] opSplit = opVal.Split('*');
                if (opSplit.Length != 2)
                {
                    throw new ArgumentException("Invalid op value given");
                }
                var op1 = getVar(opSplit[0].Trim());
                var op2 = getVar(opSplit[1].Trim());

                int op1Int;
                int op2Int;
                if (!int.TryParse(op1, out op1Int))
                {
                    throw new ArgumentException("Invalid op1 value provided");
                }

                if (!int.TryParse(op2, out op2Int))
                {
                    throw new ArgumentException("Invalid op2 value provided");
                }
                result = op1Int * op2Int;
            }
            else if (opVal.Contains("/")) //division
            {
                string[] opSplit = opVal.Split('/');
                if (opSplit.Length != 2)
                {
                    throw new ArgumentException("Invalid op value given");
                }
                var op1 = getVar(opSplit[0].Trim());
                var op2 = getVar(opSplit[1].Trim());

                int op1Int;
                int op2Int;
                if (!int.TryParse(op1, out op1Int))
                {
                    throw new ArgumentException("Invalid op1 value provided");
                }

                if (!int.TryParse(op2, out op2Int))
                {
                    throw new ArgumentException("Invalid op2 value provided");
                }
                result = op1Int / op2Int;
            }
            else
            {

                int op1Int;
                if (!int.TryParse(opVal, out op1Int))
                {
                    throw new ArgumentException("Invalid op1 value provided"); //error for invalid
                }

                result = op1Int; //if no operator found
            }

            return result;
        }

        private bool ConditionChecker(string condif)  //trying for cond like c < 10 etc
        {
            string[] parts = condif.Split(' ');
            if (parts.Length != 3)
            {
                throw new Exception("Invalid"); 
            }
            string varName = parts[0];
            string operatr = parts[1];
            int opval;
            if (!int.TryParse(parts[2], out opval))
                { throw new Exception("Invalid"); }
            if(!variables.ContainsKey(varName))
            {
                throw new Exception("Invalid");
            }
            int varValue = variables[varName];
            
            //condition check
            switch(operatr){
                case "==": return varValue == opval;
                case "!=": return varValue != opval;
                case "<": return varValue < opval;
                case ">": return varValue > opval;
                //adding more after checking run
                default:
                    throw new ArgumentException("Invalid operator given");

            }
            
        }

        private void SkipUntilEndif(string[] comLines)
        {
            // Skip lines until the corresponding endif is found
            int ifCount = 1;
            foreach (string line in comLines)
            {
                if (line.StartsWith("if"))
                {
                    ifCount++;
                }
                else if (line.StartsWith("endif"))
                {
                    ifCount--;
                    if (ifCount == 0)
                    {
                        break;
                    }
                }
            }
        }
    
}

    public class CommandEntry
    {
        public string CmdRg { get; set; }
        public ICmd Command { get; set; }
    }

    public static class CommandParser
    {

        public static void Parse(string command, List<CommandEntry> cmdList)
        {
            bool similar = false;
            foreach (var entry in cmdList)
            {
                var match = Regex.Match(command, entry.CmdRg); //matching command to the regular expression pattern
                if (match.Success)
                {
                    entry.Command.Excecute(match.Groups);  //execute the command if found
                    similar = true;
                    break;
                }
            }

            if (!similar)
                throw new InvalidDataException("Command not valid");  //show error message if not found or matched
        }
    }
}