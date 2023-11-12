using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public interface ICmd  //command execution interface
    {
        void Excecute(GroupCollection group);  //method that is implemented by classes using this interface
    }
}
