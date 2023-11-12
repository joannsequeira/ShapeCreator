using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ShapeCreator
{
    public partial class Form1 : Form
    {

        private Bitmap b;
        private Shape Shapes;
        private CmdLists cmdList;


        public Form1()
        {
            InitializeComponent();
            Shapes = new Shape(Graphics.FromImage(b));
            cmdList = new CmdLists(Shapes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }


      

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                string textUse = File.ReadAllText(ofd.FileName);
                textBox1.Text = textUse;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StreamWriter writeIt = new StreamWriter(@"P:\My Documents\Desktop\SaveTxt.Txt");
            writeIt.Write(textBox2.Text);
            writeIt.Write(textBox1.Text);
            writeIt.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSet.ClearForm(this);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
                Graphics g = e.Graphics;
                g.DrawImageUnscaled(b, 0, 0);
               
               


          }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var com = textBox2.Text;
                if (string.IsNullOrEmpty(com))
                    return;
                parseCom(com);
                textBox2.Text = " ";
                Refresh();

            }
        }

        public void parseCom(string com)
        {
            try
            {
                cmdList.Parse(com);

            }

            catch (InvalidDataException e)
            {
                MessageBox.Show(e.Message, @"Error in Command");
                   
            }
        }

    }
}
