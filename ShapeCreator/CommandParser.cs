using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

/*namespace ShapeCreator
{

    public class CommandParser
    {
         public List<CommandEntry> CList { get; internal set; } //stores list of commands

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
                 new CommandEntry { CmdRg = @"drawTri (\d+) (\d+) (\d+)", Command = new TriClass(shape)}, //Draw Triangle
                 new CommandEntry { CmdRg = @"If (\w+)", Command = new IfCond("condition", this,shape)},
                 //new CommandEntry { CmdRg = @"setVar (\w+) (\d+)", Command = setVar(shape)},
                 //new CommandEntry { CmdRg = @"While (.+)", Command = WhileCond(shape)},
             };

         }

         public void Parse(string com)
         {
             CommandParser.Parse(com, CList);  //parse coammand using the specified list
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
                     //if (entry.Command is IfCond ifcond)
                     //{
                         //ifcond.Excecute(match.Groups, command);

                     //}
                     //else if (entry.Command is WhileCond whilecond)
                     //{
                         //WhileCond.Excecute(match.Groups, command);
                     //}
                     //else
                      entry.Command.Excecute(match.Groups);  //execute the command if found
                     similar = true;
                     break;
                 }
             }

             if (!similar)
                 throw new InvalidDataException("Command not valid");  //show error message if not found or matched
         }
     } 

        public ArrayList LinePointer = new ArrayList();

        public string[] val;


        public void Excecuter(ArrayList abc, string[] strings, int counter, Graphics g)
        {
            int lc = 0;
            int jc = 0;


            Pen p = new Pen(Color.Black);
            Brush b = new SolidBrush(Color.Black);

            while (lc < strings.Length)
            {
                try
                {
                    if (jc != 0 && jc == lc)
                    {
                        lc++;
                        jc = 0;
                    }

                    val = (string[])abc[lc];
                    switch (val[0].ToLower())
                    {
                        case "Circle":
                            int radius = VarCall(val[1]);
                            new CircClass(x, y, radius).ShapeDrawer(g, p, b);

                        default:
                            break;
                    }



                }
                }
        }
    } 
    } */
























