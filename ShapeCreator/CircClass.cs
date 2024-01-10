using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    //Class to draw circle
    public class CircClass : MainShape
    {
        //constructor class
        /* public CircClass(Shape shape) : base(shape) { 
        
        }

        //implementing methods of the base class
        public override void Excecute(GroupCollection group)
        {
            if (group.Count < 2)
            {
                throw new ArgumentException("Insufficient Parameters");
            }
            Shapes.DrawCirc(int.Parse(group[1].Value)); //extracting value of radius from the regular expression groups
        }
    } */
        int radius;

        

        public CircClass(int x, int y,int radius) : base(x,y)
        {
            this.radius = radius;

        }

        public CircClass(int radius)
        {
            this.radius = radius;
        }

        public override void ShapeDrawer(Graphics g, Pen p, Brush b)
        {
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
            g.FillEllipse(b,x, y, radius * 2, radius * 2);

        }

        public override void ListShape(params int[] listShape)
        {
            base.ListShape(listShape[0], listShape[1]);
            this.radius = listShape[2];
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.radius; 
        }

    }


}
