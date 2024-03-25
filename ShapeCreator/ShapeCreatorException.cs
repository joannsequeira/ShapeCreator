using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace ShapeCreator
{
    public class ShapeCreatorException : Exception
    {

        public int line;

        public ShapeCreatorException(string message)
           : base(message)
        {

        }

        public ShapeCreatorException(string message, int line1)
            : base(message)
        {
            this.line = line1 + 1;
        }

        
    }
}
