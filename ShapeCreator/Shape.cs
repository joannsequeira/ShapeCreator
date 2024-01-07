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
        public Dictionary<string, int> vars = new Dictionary<string, int>();
        public CmdLists cmdLists;


        public Shape(Graphics g)

        {
            this.g = g;
            this.cmdLists = cmdLists;
            x = 0;
            y = 0;
            pn = new Pen(Color.Black, 3);
            sb = new SolidBrush(Color.Black);
        }

        public void PenPos(int x, int y)  //Method to change cursor pos
        {

            this.x = x;
            this.y = y;
        }

        public void PenChange(String color)  //Change pen color
        {
            var colr = Color.FromName(color);
            pn = new Pen(colr, 3);

        }

        public void BrushChange(String color)  //Change brush color
        {
            var colr = Color.FromName(color);

            sb = new SolidBrush(colr);
        }

        public void Clearsc() //Clearing the screen
        {
            g.Clear(Color.Transparent);  
        }

        public void ResetPos()  //Reset the cursor to 0,0
        {
            x = y = 0;
        }


        public void DrawRect(int width, int height) {   //Draw Rectangle
            var rect = new Rectangle(x, y, width, height);

            if (oil)   
            {
                g.FillRectangle(sb, rect); //if oil true, Fill rectangle
            }

            else

                g.DrawRectangle(pn, rect); //if oil false, only draw outline 
        }

        public void FillShape(bool oilp)
        {
            oil = oilp;  //Set value of oil from Fill command
        }

        public void DrawCirc(int radius) //Draw Circle
        {

            if (oil)
            {
                g.FillEllipse(sb, x, y, 2 * radius, 2 * radius); //if oil true, Fill Circle
            }
            else
            {
                g.DrawEllipse(pn, x, y, 2 * radius, 2 * radius); //if oil false, draw only outline
            }
        }

        public void DrawTo(int a, int b)  //Draw line to specified points
        {
            g.DrawLine(pn,x, y, a,b);
            x = a;
            y = b;
        }

        public void DrawTri(int i, int j, int k)
        {
            Point[] points = { new Point(x,y), new Point(x + i, y), new Point(x +j, y+k) //form the triangle vertices cosidering x,y pos

           };

            if(oil)
            {
                g.FillPolygon(sb, points);
            }
            else
                g.DrawPolygon(pn, points);
        }

        public void SetVar(string name, int value)
        {
            vars[name] = value;
        }

        public int GetVar(string name)
        {
            if (vars.TryGetValue(name, out int value))
            {
                return value;
            }
            throw new ArgumentException($"Variable {name} not found");
        }


        public void InsideIf(string command)
        {
            string block = command.Substring(command.IndexOf("If") + 2);
            string[] commands = block.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

            foreach(var cmd in commands)
            {
                CommandParser.Parse(cmd.Trim(), cmdLists.CList);
            }
        }

    }
        

    }
    


