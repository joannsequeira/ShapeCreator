using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    //Class to draw circle
    public class CircClass : TrialBase   //extends TrialBase
    {
        //constructor class
        public CircClass(Shape shape) : base(shape) { 
        
        }

        //implementing methods of the base class
        public override void Excecute(GroupCollection group)
        {
            if (group.Count < 2)
            {
                throw new ShapeCreatorException("Insufficient Parameters");
            }
            Shapes.DrawCirc(int.Parse(group[1].Value)); //extracting value of radius from the regular expression groups
        }
    }
}
