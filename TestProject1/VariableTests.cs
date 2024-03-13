using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeCreator;

namespace VariableTests
{
    
        [TestClass]
        public class VariableTests
        {

            [TestMethod]
            public void validVariableSetTest()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "a = 5";

                commandParser.Parse(command);

                Assert.AreEqual(5, commandParser.variables["a"]);

            }

            [TestMethod]
            public void validVariableWithAddTest()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "b = 5\nb = b + 50";

                commandParser.Parse(command);

                Assert.AreEqual(55, commandParser.variables["b"]);

            }

            [TestMethod]
            public void validVariableWithSubTest()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "b = 90\nb = b - 50";

                commandParser.Parse(command);

                Assert.AreEqual(40, commandParser.variables["b"]);

            }

            [TestMethod]
            public void variableWithAddandComTest()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "b = 5\nb = b + 50\n DrawCirc b";

                commandParser.Parse(command);

                Assert.AreEqual(55, commandParser.variables["b"]);

            }

            [TestMethod]
            public void createVariableAndUseinDraw()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "b = 5\nc = 9\ndrawRect b c";

                commandParser.Parse(command);

                Assert.AreEqual(5, commandParser.variables["b"]);
                Assert.AreEqual(9, commandParser.variables["c"]);



        }

            [TestMethod]
            public void invalid_createVariableAndUseinDraw()

            {

                Shape shape = new Shape(null);
                CmdLists commandParser = new CmdLists(shape);

                var command = "b = 5\ndrawCirc c";

                ShapeCreatorException ex = Assert.ThrowsException<ShapeCreatorException>(() => commandParser.Parse(command));
                Assert.AreEqual("drawCirc c Command not valid", ex.Message);

            }


        }
    }





