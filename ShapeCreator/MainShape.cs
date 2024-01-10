using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ShapeCreator
{
    public abstract class MainShape : ICmd
    {

        protected int x,y;

        public MainShape(int x, int y)
        {
            this.x = x;
            this.y = y;

        }

        public MainShape() { }
        public abstract void ShapeDrawer(Graphics g, Pen p, Brush b);

        public virtual void ListShape(params int[] listShape)
        {
            this.x= listShape[0];
            this.y = listShape[1];
        }

        

    }
}




