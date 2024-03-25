 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Threading;

namespace ShapeCreator
{

    public class CmdLists
    {
        
        public Dictionary<string, int> variables; //store variable names and values
        public Dictionary<string, string> mthdDeclare; //store methods 

        Shape shape;
        public CmdLists(Shape shape) //constructor used for command intialisation
        {
            this.shape = shape;

        }

        /// <summary>
        /// Parse the commands line by line given as input text
        /// </summary>
        /// <param name="com">The command string to parse </param>
        /// <exception cref="ArgumentException">Thrown for invalid variable intialisation format</exception>
        public void Parse(string com)
        {
            variables = new Dictionary<string, int>();  //add variables to dictionary
            mthdDeclare = new Dictionary<string, string>(); //add method to dictionary

            string[] comLines = com.Split('\n'); //split commands based on next line
            bool inIfBlock = false; //flat for inside if
            bool skipIfBlock = false;  //flag for skip if condition not true

            //flags for loop 
            bool inLoopBlock = false;
            bool skipLoopBlock= false;
            int lpCountr = 0;

            //flags and variables for method handling
            bool inMthdBlock = false;
            var mthdVar = "";
            var mthdName = "";



            for (int lineCounter = 0; lineCounter < comLines.Length; lineCounter++)
            {
                var line = comLines[lineCounter];
                line = line.Trim();
                //Thread.Sleep(1000);  //thread sleep  to show 

                

                    if (line.StartsWith("if") || line.StartsWith("endif") || skipIfBlock) //handle if, endif and skpping lines in block
                    {
                        inIfBlock = true;  //inside if block
                        if (line.StartsWith("endif"))
                        {
                            inIfBlock = false; //outside if block
                            skipIfBlock = false; 
                            continue;
                        }
                        else if (skipIfBlock)
                        {
                            continue;  
                        }
                        else
                        {
                            string condif = line.Substring(3).Trim(); //extract condition
                            if (!ConditionChecker(condif, lineCounter))  //check condition
                            {
                                skipIfBlock = true;  //if not true, skip lines till end if
                            }

                        }
                    }
               else if (line.StartsWith("loop") || line.StartsWith("endloop") || skipLoopBlock) //handle loop, endloop and repeating lines in block
                {
                    inLoopBlock = true;

                    if (line.StartsWith("endloop"))
                    {

                        if (!skipLoopBlock)
                        {
                            lineCounter = lpCountr - 1;  //repeat loop till condition is false
                        }
                        else
                        {
                            inLoopBlock = false;
                            skipLoopBlock = false;
                        }
                        continue;
                    }
                    else if (skipLoopBlock)
                    {
                        continue; //skip lines till end of loop
                    }
                    else if (line.StartsWith("loop"))
                    {



                        lpCountr = lineCounter;  //allow line to repeat in block
                        string condLoop = line.Substring(4).Trim();
                        if (!ConditionChecker(condLoop, lineCounter))
                        {
                            skipLoopBlock = true; //if not true, skip lines till end if
                        }
                    }
                }

                else if (line.StartsWith("mthd") || line.StartsWith("endmthd") || inMthdBlock)  //handle method definition and call
                {
                    if (line.StartsWith("endmthd"))
                    {
                        inMthdBlock = false;
                        mthdDeclare.Add(mthdName, mthdVar);  //once endmthd is found, add to Dictionary
                        continue;
                    }

                    else if (line.StartsWith("mthd"))
                    {
                        string[] parts = line.Split(' ');
                        mthdName = parts[1].Trim();

                        if (mthdDeclare.ContainsKey(mthdName))
                        {
                            throw new ShapeCreatorException("Method already exist.", lineCounter);  //create method with same name
                        }
                        inMthdBlock = true;  //set flag to inside block
                    }
                    else
                    { 
                     if  (mthdVar.Length > 0) {
                      mthdVar = mthdVar + '\n' + line;  //for methods with multiple lines

                    }
                    else
                    {
                        mthdVar = line;  
                    }
                }
                }
                else if (line.StartsWith("callmthd"))
                {
                    string[] mthdCall = line.Split(' ');  
                    string mthdCallName = mthdCall[1].Trim();

                    if (!mthdDeclare.ContainsKey(mthdCallName))
                    {
                        throw new ShapeCreatorException("Method does not exist.", lineCounter);
                    }

                    var mthdCom = mthdDeclare[mthdCallName];  //commands loaded into mthdCom from Dictionary
                    Parse(mthdCom);
                }

                else if (line.Contains("="))
                    {
                        string[] parts = line.Split('='); //check line for "=" and split the line to parts
                        if (parts.Length == 2)
                        {
                            string varName = parts[0].Trim(); //store first part as var name


                            string opVal = parts[1].Trim();  
                            int result = Op(opVal, lineCounter); //process value 


                            VariableHandler(varName, result);
                        }
                        else
                        {
                            throw new ShapeCreatorException("Format for variable is wrong", lineCounter);
                        }
                    }

                    else if (line.Length > 0)
                {
                        ExcecuteCom(line);
                    }
                
            if (lineCounter == comLines.Length - 1) //check the last statement
            {
                //check for end statement when if,loop,mthd used 
                //throw exceptions if not found
                if (!line.StartsWith("endif") && inIfBlock)
                {
                    throw new ShapeCreatorException("endif does not exist.", lineCounter);
                }
                if (!line.StartsWith("endloop") && inLoopBlock)
                {
                    throw new ShapeCreatorException("endloop does not exist.", lineCounter);
                }
                if (!line.StartsWith("endmthd") && inMthdBlock)
                {
                    throw new ShapeCreatorException("endmthd does not exist.", lineCounter);
                }
            }

        }

        //CommandParser.Parse(com, CList);  //parse coammand using the specified list
    }

        
        /// <summary>
        /// Assigns variable names to given value
        /// </summary>
        /// <param name="varName"> Variable for name</param>
        /// <param name="varValue">Variable for value</param>
        
        private void VariableHandler(string varName, int varValue)
        {
            variables[varName] = varValue; //store value with corresponding varname
        }

        /// <summary>
        /// Replaces variable name with value and calls the Command parser parse method to execute the command
        /// </summary>
        /// <param name="com">The command that needs to be excecuted</param>
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
            new CommandParser(shape).Parse(newCom);
        }

        /// <summary>
        /// get value of variable from dictionary or return input string if not found
        /// </summary>
        /// <param name="stmt">variable name or input string</param>
        /// <returns> The value of the variable or input string if no value is found</returns>
        private string getVar(string stmt) //get val from dictionary or return input string com
        {
            if (variables.ContainsKey(stmt))
            {
                return variables[stmt].ToString(); 
            }
            return stmt;  //if not found
        }


        /// <summary>
        /// Find the operator in input string and perform the corresponding arithmetic operation
        /// </summary>
        /// <param name="opVal">Input string containing the operator to find</param>
        /// <returns>The result after operation is performed</returns>
        /// <exception cref="ShapeCreatorException">Invalid operator provided</exception>
        private int Op(string opVal, int lineCounter)  //find arithmetic operator and handle result
        {
            char[] ops = {'+', '-', '*', '/'}; //array of arithmetic operators
            char optr = default(char);
            


            foreach (char op in ops)
            {
                if (opVal.Contains(op))
                {
                    optr = op; break;    //asign value of operator used to optr
                }
            }
            if (optr == default(char))  //if there is no operator 
            {
                if (!int.TryParse(getVar(opVal), out int result))
                
                    throw new ShapeCreatorException("Invalid Operator Provided", lineCounter);
                return result;
                
            }

                    string[] opSplit = opVal.Split(optr);
                    if(opSplit.Length != 2)
                    
                        throw new ShapeCreatorException("Invalid operator input provided", lineCounter);
                    if (!int.TryParse(getVar(opSplit[0].Trim()), out int op1Int) || !int.TryParse(getVar(opSplit[1].Trim()), out int op2Int))
                        throw new ShapeCreatorException("Invalid operation format or assignment", lineCounter);
            
                    switch(optr)
                    {
                        case '+':  return op1Int + op2Int; 
                           
                        case '-': return op1Int - op2Int; 
                        case '*': return op1Int * op2Int; 
                        case '/': 
                            if(op2Int == 0)
                            {
                                throw new ShapeCreatorException("Division By Zero", lineCounter);
                            }
                            return op1Int / op2Int;
                          

                        default: throw new ShapeCreatorException("Invalid Operator", lineCounter);
                    }
                
                
            }

        /// <summary>
        /// Check if format is variavle cond value
        /// </summary>
        /// <param name="condif">Condition that needs to be checked</param>
        /// <returns>If the condition is true or false</returns>
        /// <exception cref="Exception">Invalid Format</exception>
        /// <exception cref="ShapeCreatorException">Invalid Conditonal Operator</exception>
        private bool ConditionChecker(string condif, int lineCounter)  //trying for cond like c < 10 etc
        {
            string[] parts = condif.Split(' ');
            if (parts.Length != 3)
            {
                throw new ShapeCreatorException("Invalid condition format", lineCounter); 
            }
            string varName = parts[0];
            string operatr = parts[1];
            int opval;
            if (!int.TryParse(parts[2], out opval))
                { throw new ShapeCreatorException("Invalid value provided", lineCounter); }
            if(!variables.ContainsKey(varName))
            {
                throw new ShapeCreatorException("Invalid varname provided", lineCounter);
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
                    throw new ShapeCreatorException("Invalid operator given", lineCounter);

            }
            
        }
  
}

    public class CommandEntry
    {
        public string CmdRg { get; set; }
        public ICmd Command { get; set; }
    }

    public class CommandParser
    {
        public List<CommandEntry> CList { get; internal set; } //stores list of commands
        public CommandParser(Shape shape) //constructor used for command intialisation
        {
            CList = new List<CommandEntry>
            {
                new CommandEntry { CmdRg = @"clearsc", Command = new ClearScreen(shape)  },  //Clear Screen
                new CommandEntry { CmdRg = @"drawRect (\d+) (\d+)$", Command = new RectClass(shape)  },  //Draw Rectangle
                new CommandEntry { CmdRg = @"colorin (true|false)", Command = new FillShape(shape)} ,  //Fill Shape
                new CommandEntry { CmdRg = @"resetPos", Command = new ResetPos(shape)},  //Reset Pointer
                new CommandEntry { CmdRg = @"penChange (\w+)$", Command = new PenChange(shape)}, //Change color of Pen
                new CommandEntry { CmdRg = @"brushChange (\w+)$", Command = new BrushChange(shape)}, //Change color of Brush
                new CommandEntry { CmdRg = @"drawCirc (\d+)$", Command = new CircClass(shape)},  //Draw Circle
                new CommandEntry { CmdRg = @"drawTo (\d+) (\d+)$", Command = new DrawTo(shape)}, //Draw Line
                new CommandEntry { CmdRg = @"moveTo (\d+) (\d+)$", Command = new PenPos(shape)}, //Move pointer
                new CommandEntry { CmdRg = @"drawTri (\d+) (\d+) (\d+)$", Command = new TriClass(shape)} //Draw Triangle

            };

        }
        public void Parse(string command)
        {
            bool similar = false;
            foreach (var entry in CList)
            {
                var match = Regex.Match(command, entry.CmdRg, RegexOptions.IgnoreCase); //matching command to the regular expression pattern
                if (match.Success)
                {
                    entry.Command.Excecute(match.Groups);  //execute the command if found
                    similar = true;
                    break;
                }
            }

            if (!similar)
                throw new ShapeCreatorException(command+" Command not valid");  //show error message if not found or matched
        }
    }
}