﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public abstract class TrialBase : ICmd 
    {
        

        protected readonly Shape Shapes;

        protected TrialBase(Shape sh)
        {
            Shapes = sh;
        }

        public void Excecute(GroupCollection group)
        {
            throw new NotImplementedException();
        }

        public abstract void Execute(GroupCollection @group);

        protected int IntParseGroup(GroupCollection @group, int id)
        {
            return int.Parse(group[id].Value);
        }
    }




   
}
