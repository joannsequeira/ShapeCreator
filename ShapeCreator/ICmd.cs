using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ShapeCreator
{
     interface ICmd  //command execution interface
    {
        /// <summary>
        /// void Excecute(GroupCollection group);  //method that is implemented by classes using this interface
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>

        void ShapeDrawer(Graphics g, Pen p, Brush b);

        void ListShape(params int[] listShape);

        void SetTriangle(int x, int y, Point[] pt);


    }


}
