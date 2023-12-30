using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    public class IfCond : ICmd
    {
        public string VarName { get; set; }
        public int VarValue { get; set; }
        public List<ICmd> CondCom {  get; set; } = new List<ICmd>();
        public void Excecute(GroupCollection group)
        {
            VarName = group[1].Value;
            VarValue = int.Parse(group[2].Value);

            if(CondCom.Count > VarValue ) 
            {
                foreach (var command in CondCom )
                {
                    command.Excecute(group);
                }

            }



            throw new NotImplementedException();
        }
    }
}
