using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{

    //Class extending TrialBase, handling drawing rectangle


    public class RectClass : TrialBase

    {
        //Constructor class
        public RectClass(Shape shape) : base(shape) { 
        }

        public override void Excecute(GroupCollection @group) //Implementing execute method from the inherited class
        {
            if (group.Count < 3)
            {
                throw new ArgumentException("Insufficient Parameters");
            }
            Shapes.DrawRect(IntParseGroup(group,1), IntParseGroup(group, 2)); //getting width, height from reg. expression groups

        }

    }
}
