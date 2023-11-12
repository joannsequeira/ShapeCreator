using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public abstract class TrialBase : ICmd //class for command execution
    {
        

        protected readonly Shape Shapes; //stores shape object

        protected TrialBase(Shape sh)  //constructor class
        {
            Shapes = sh;
        }

        

        public abstract void Excecute(GroupCollection @group); // Abstract method needed to implement using derived classes for command execution

        protected int IntParseGroup(GroupCollection @group, int id)
        {
            return int.Parse(group[id].Value); //converts matched group to an integer
        }
    }




   
}
