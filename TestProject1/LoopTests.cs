﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeCreator;

namespace LoopTests
{
    [TestClass]
    public class LoopTests
    {
        /// <summary>
        /// Loop to test invalid(not true) condition 
        /// </summary>
        [TestMethod]
        public void invalidLoopCondition()

        {
            var command = "a = 50\r\n loop a > 100\r\n drawCirc 80\r\n a = a + 10 \r\nendloop";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }

        /// <summary>
        /// Loop to test valid(true) condition 
        /// </summary>

        [TestMethod]
        public void validLoopCondition()

        {
            var command = "a = 5\r\nloop a < 10\r\ndrawCirc a\r\na = a + 10\r\nendloop";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }

        /// <summary>
        /// Loop to test if loop block missing endloop
        /// </summary>

        [TestMethod]
        

        public void invalidLoopCodeBlock_MissingEndLoop()

        {
            var command = "a = 5\r\nloop a < 10\r\ndrawCirc a\r\na = a + 10\r\n";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);


            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("endloop does not exist.", ex.Message);

        }
    

}
}
