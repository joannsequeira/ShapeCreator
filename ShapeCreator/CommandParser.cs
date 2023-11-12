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
        public List<CommandEntry> CList { get; internal set; }

        public CmdLists(Shape shape)
        {
            CList = new List<CommandEntry>
            {
                new CommandEntry { CmdRg = @"clearsc", Command = new ClearScreen(shape)  },
                new CommandEntry { CmdRg = @"drawRect (\d+) (\d+)", Command = new RectClass(shape)  },
                new CommandEntry { CmdRg = @"colorin (true|false)", Command = new FillShape(shape)} ,
                new CommandEntry { CmdRg = @"ResetPos", Command = new ResetPos(shape)},
                new CommandEntry { CmdRg = @"penChange (\w+)", Command = new PenChange(shape)},
                new CommandEntry { CmdRg = @"brushChange (\w+)", Command = new BrushChange(shape)},
                new CommandEntry { CmdRg = @"drawCirc (\d+)", Command = new CircClass(shape)},
                new CommandEntry { CmdRg = @"drawTo (\d+) (\d+)", Command = new DrawTo(shape)},
                new CommandEntry { CmdRg = @"moveTo (\d+) (\d+)", Command = new PenPos(shape)},


            };

        }

        public void Parse(string com)
        {
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
                var match = Regex.Match(command, entry.CmdRg);
                if (match.Success)
                {
                    entry.Command.Excecute(match.Groups);
                    similar = true;
                    break;
                }
            }

            if (!similar)
                throw new InvalidDataException("Command not valid");
        }
    }
}



    





            
    

    
    
    



