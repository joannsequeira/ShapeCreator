using ShapeCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace MethodTests
{
    [TestClass]
    public class MethodTests
    {
        [TestMethod]
        public void validMethodTest()

        {
            var command = "mthd testm1\r\ndrawCirc 80\r\nendmthd\r\n\r\ncallmthd testm1";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            commandParser.Parse(command);

            Assert.AreEqual("0 0", shape.getPenPos());

        }


        [TestMethod]
        public void invalidMethodCallTest()

        {
            var command = "mthd test\r\ndrawCirc 80\r\nendmthd\r\n\r\ncallmthd test1";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("Method does not exist.", ex.Message);

        }

        [TestMethod]
        public void mssingMethodEndBlockTest()

        {
            var command = "mthd test\r\ndrawCirc 80\r\ncallmthd test";

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("endmthd does not exist.", ex.Message);

        }
    }
}
