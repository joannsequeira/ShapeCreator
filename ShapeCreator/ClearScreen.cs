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

        public ClearScreen(Shape sh) : base(sh)
        {

        }

        public override void Execute(GroupCollection group)
        {
            Shapes.Clearsc();
        }
    }
}
