using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


///<summary>
///This class represents the classes and constructor classes of Fill, Change pen and brush color, drawto and moveto
///Extracting values from the regular expressions groups 
/// </summary>



namespace ShapeCreator
{
    public abstract class FillShape : ICmd
    {
        protected int x, y;

        public FillShape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public FillShape()
        { }

        public abstract void ShapeDrawer(Graphics g, Pen p, Brush b);

        public virtual void SetTriangle(int x, int y, Point[] pt);

        public override string ToString()
        {
            return base.ToString() + "  " + this.x + ", " + this.y + ":";
        }


        void ListShape(params int[] listShape);

    }
}




