using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCreator
{
    public class Shape
    {
        Graphics g;
        Pen pn;
        int x, y;
        private bool oil = false;
        SolidBrush sb = null;


        public Shape(Graphics g)

        {
            this.g = g;
            x = 0;
            y = 0;
            pn = new Pen(Color.Black, 3);
        }

        public void PenPos(int x, int y)
        {

            this.x = x;
            this.y = y;
        }

        public void PenChange(String color)
        {
            var colr = Color.FromName(color);
            pn = new Pen(colr, 3);
            sb = new SolidBrush(colr);
        }

        public void DrawRect(int width, int height) { 
                  var Rect = new Rectangle(x, y, width, height);

            if (oil)
            {
                g.FillRectangle(sb, Rect);
            }

            else
                g.FillEllipse(sb, Rect);
            }
        
        }
        

         
        
       


    }

