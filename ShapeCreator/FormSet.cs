using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeCreator
{
    internal class FormSet
    {
        public static void ClearForm(Control form) 
        { 
          foreach (Control cont in form.Controls) 
            
           {
            if (cont is TextBox)
                {
                    TextBox textBox = (TextBox)cont;
                    textBox.Text = null;
                }
                if (cont is PictureBox)
                {
                    PictureBox pBox = (PictureBox)cont;
                    pBox.Image = null;

                }
            }
        }
    }
}
