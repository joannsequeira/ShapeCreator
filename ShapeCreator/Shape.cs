﻿using System;
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
        Boolean syntaxCheck = false;
        

        public Shape(Graphics g, Boolean syntaxCheck)

        {
            this.g = g;
            x = 0;
            y = 0;
            pn = new Pen(Color.Black, 3);
            sb = new SolidBrush(Color.Black);
            this.syntaxCheck = syntaxCheck;
        }

        public void PenPos(int x, int y)  //Method to change cursor pos
        {

            this.x = x;
            this.y = y;
        }

        public string getPenPos()
        {
            return this.x + " " + this.y;
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
            if (syntaxCheck)
            {
                return;
            }

            g.Clear(System.Drawing.SystemColors.ButtonShadow);  
        }

        public void ResetPos()  //Reset the cursor to 0,0
        {
            x = y = 0;
        }


        public void DrawRect(int width, int height) {   //Draw Rectangle
            var rect = new Rectangle(x, y, width, height);
            if (syntaxCheck)
            {
                return;
            }

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
            if (syntaxCheck)
            {
                return;
            }

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
            if (syntaxCheck)
            {
                return;
            }
            g.DrawLine(pn,x, y, a,b);
            x = a;
            y = b;
        }

        public void DrawTri(int i, int j, int k)
        {
            if (syntaxCheck)
            {
                return;
            }
            Point[] points = { new Point(x,y), new Point(x + i, y), new Point(x +j, y+k) //form the triangle vertices cosidering x,y pos

           };

            if(oil)
            {
                g.FillPolygon(sb, points);
            }
            else
                g.DrawPolygon(pn, points);
        }

        /// <summary>
        /// Draw Random shapes with delay effect
        /// </summary>
        /// <param name="random">Generate Random numbers for shape parameters</param>
        /// <param name="x"> x coordinate for drawing</param>
        /// <param name="y">y coordinate for drawing</param>
        public void ShapeAni(Random random, int x, int y)
        {
           
                int circSize = random.Next(20, 100); //radius
                int rectW = random.Next(20, 200); //rect width
                int rectH = random.Next(20, 200);  //rect height
                int triPts = random.Next(20, 100); //triangle points

                Clearsc();

                // Draw a rectangle
                PenPos(x + 50, y); // Adjust the position for each shape to prevent overlap
                DrawRect(rectW, rectH);
                System.Threading.Thread.Sleep(2000);

                // Draw a circle
                PenPos(x, y + 100);
                DrawCirc(circSize);
                System.Threading.Thread.Sleep(1000);

                // Draw a triangle
                PenPos(x + 100, y + 100);
                DrawTri(triPts, triPts, triPts);

                // Add some delay to observe the animation
                System.Threading.Thread.Sleep(3000);
            
        }



    }


}

