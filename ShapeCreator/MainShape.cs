using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShapeCreator
{
    public class FillShape : ICmd
    {
        private readonly Shape shape;

        public FillShape(Shape shape) 
        {
            this.shape = shape;
        }

        public void Excecute(GroupCollection group) {
            var oil = group[1].Value;
            shape.FillShape(oil == "true");

        }
    }


    public class PenChange : TrialBase
    {
        
        public PenChange(Shape shape) : base(shape)
        {

        }

        public override void Excecute(GroupCollection group)
        {
            var colr = group[1].Value;
            Shapes.PenChange(colr);
        }
    }


   


}




