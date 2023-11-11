using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{

    public class CmdLists
    {
        public List<L> CList { get; internal set; }

        public CmdLists(Shape shape)
        {
            CList = new List<L>
            {
                new L { CdRg = @"clearsc", Command = new Clearsc(shape)  }
            };

        }

        public void Parse(string com)
        {
            CommandParser.Parse(com, CList);
        }

    }

    public static class CommandParser
    {
        public static void Parse(string command, List<L> cmdList)
        {
           var similar = false;
            cmdList.ForEach(L =>
            {
                var com = Regex.Match(command);
                if (command.Success)
                {

                    similar = true;


                }
            });
            if (!similar)
                throw new InvalidDataException("Command not valid");


        }
    } 







}

    
    
    



