using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public class ClearScreen : TrialBase
    {

        public ClearScreen(Shape shape) : base(shape)
        {

        }

        public override void Execute(GroupCollection group)
        {
            Shapes.Clearsc();
        }
    }
}
