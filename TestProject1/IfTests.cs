using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeCreator;

namespace IfTests
{
    [TestClass]
    public  class IfTests
    {
        [TestMethod]
        public void inValidIfCondition()

        {
            var command = "a = 50\r\nif a < 5\r\n drawCirc 50\r\nendif";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }

        [TestMethod]
        public void validIfCondition()

        {
            var command = "a = 1\r\nif a < 5\r\n drawCirc 50\r\nendif";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }
        [TestMethod]
        public void validnestedIfCondition()

        {
            var command = "a = 1\r\nif a < 5\r\n drawCirc 50\r\n if a > 5\r\n drawCirc 50\r\n endif \r\n endif";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }


        [TestMethod]
        
        public void inValidIfCodeBlock_MissingEndIf()

        {
            var command = "a = 1\r\nif a < 5\r\n drawCirc 50\r\n";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);


            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("endif does not exist.", ex.Message);

        } 
    }
}
