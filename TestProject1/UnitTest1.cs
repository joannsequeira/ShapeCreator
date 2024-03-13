using ShapeCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


//https://learn.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2022&tabs=dotnet%2Cmstest

namespace SingleCommandTest
{
    [TestClass]
    public class SingleCommandTest
    {
        [TestMethod]
        public void validCircleDraw()

        {
            var command = " drawCirc 50";

            Shape shape = new Shape(null);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);


        }
    }
}