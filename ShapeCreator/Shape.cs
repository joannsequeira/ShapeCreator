using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public class Shape
    {
        Graphics g;
        Pen pn;
        int x, y;
        private bool oil = false;
        SolidBrush sb;


        public Shape(Graphics g)

        {
            this.g = g;
            x = 0;
            y = 0;
            pn = new Pen(Color.Black, 3);
            sb = new SolidBrush(Color.Black);
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

        public void Clearsc()
        {
            g.Clear(Color.Transparent);
        }

        public void ResetPos()
        {
            x = y = 0;
        }


        public void DrawRect(int width, int height) { 
                  var rect = new Rectangle(x, y, width, height);

            //if (oil)
            //{
                g.FillRectangle(sb, rect);
            //}

            //else
                //g.FillEllipse(pn, rect);
            }
        
        }

    }

