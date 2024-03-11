using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace ShapeCreator
{
    internal class ShapeCreatorException : Exception
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

        public ShapeCreatorException(string message, Exception inner, int line)
            : base(message, inner)
        {
            this.line = line + 1;
        }
    }
}
