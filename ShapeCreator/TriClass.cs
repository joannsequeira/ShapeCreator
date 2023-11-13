using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    //Class to draw triangle
    public class TriClass : TrialBase   //extends TrialBase
    {
        //constructor class
        public TriClass(Shape shape) : base(shape)
        {

        }

        //implementing methods of the base class
        public override void Excecute(GroupCollection group)
        {
            if (group.Count < 4)
            {
                throw new ArgumentException("Insufficient Parameters");
            }
            Shapes.DrawTri(IntParseGroup(group, 1), IntParseGroup(group, 2), IntParseGroup(group, 3)); //extracting value of radius from the regular expression groups
        }
    }

}
