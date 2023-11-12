using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    public class CircClass : TrialBase
    {
        public CircClass(Shape shape) : base(shape) { 
        
        }

        public override void Excecute(GroupCollection group)
        {
            Shapes.DrawCirc(int.Parse(group[1].Value));
        }
    }
}
