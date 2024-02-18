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

            foreach(string line in comLines)
            {
                if(line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    if(parts.Length == 2)
                    {
                        string varName= parts[0].Trim(); //store first part as var name
                        int varValue;
                        if (!int.TryParse(parts[1].Trim(), out varValue))
                        {
                            throw new ArgumentException("Invalid value provided");
                        }
                        VariableHandler(varName, varValue);
                    }
                    else
                    {
                        throw new ArgumentException("Format for variable is wrong");
                    }

                }
                else
                {
                    ExcecuteCom(line.Trim());
                }
            }
            //CommandParser.Parse(com, CList);  //parse coammand using the specified list
        }
        
        private void VariableHandler(string varName, int varValue)
        {
            variables[varName] = varValue;
        }

        private void ExcecuteCom(string com) {

            string[] parts = com.Split(' '); //split command into parts
            if(parts.Length == 2 && variables.ContainsKey(parts[1])) //check for variable
            { 
               int value = variables[parts[1]]; 
               com = com.Replace(parts[1], value.ToString()); //replace name with value in command
            }
            else
            {
                for(int i= 1; i<parts.Length; i++)
                {
                    if (!int.TryParse(parts[i], out int _))
                    {
                        if (variables.ContainsKey(parts[i]))
                        {
                            int varValue = variables[parts[i]];
                            com = com.Replace(parts[i], varValue.ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            CommandParser.Parse(com, CList);
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



    





            
    

    
    
    



