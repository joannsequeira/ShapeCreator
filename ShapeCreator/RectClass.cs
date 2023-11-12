using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    public class RectClass : TrialBase

    {
        public RectClass(Shape shape) : base(shape) { 
        }

        public override void Excecute(GroupCollection @group)
        {
            Shapes.DrawRect(IntParseGroup(group,1), IntParseGroup(group, 2));

        }








    }
}
