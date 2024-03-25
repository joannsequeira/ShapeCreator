using ShapeCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxCheckTest
{
    [TestClass]
    public class SyntaxCheckTest
    {
        [TestMethod]
        public void invalid_createVariableAndUseinDraw()

        {

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            var command = "b = 5\ndrawgirc c";

            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("drawgirc c Command not valid", ex.Message);

        }

        [TestMethod]
        public void invalidformatVariableSetTest()

        {

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            var command = "a == 5";

            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("Format for variable is wrong", ex.Message);

        }

        [TestMethod]
        public void invalidformatCondSetTest()

        {

            Shape shape = new Shape(null, true);
            CmdLists commandParser = new CmdLists(shape);

            var command = "a = 50\r\n loop a -= 100\r\n drawCirc 80\r\n a = a + 10 \r\nendloop";

            ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
            Assert.AreEqual("Invalid operator given", ex.Message);

        }





    }
}
