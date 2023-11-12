using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


///<summary>
///This class represents the classes and constructor classes of Fill, Change pen and brush color, drawto and moveto
///Extracting values from the regular expressions groups 
/// </summary>



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

    public class BrushChange : TrialBase
    {

        public BrushChange(Shape shape) : base(shape)
        {

        }

        public override void Excecute(GroupCollection group)
        {
            var colr = group[1].Value;
            Shapes.BrushChange(colr);
        }
    }

    public class DrawTo : TrialBase
    {

        public DrawTo(Shape shape) : base(shape)
        {

        }

        public override void Excecute(GroupCollection group)
        {
            Shapes.DrawTo(IntParseGroup(group, 1), IntParseGroup(group, 2));
        }
    }

    public class PenPos : TrialBase
    {

        public PenPos(Shape shape) : base(shape)
        {

        }

        public override void Excecute(GroupCollection group)
        {
            Shapes.PenPos(IntParseGroup(group, 1), IntParseGroup(group, 2));
        }
    }





}




