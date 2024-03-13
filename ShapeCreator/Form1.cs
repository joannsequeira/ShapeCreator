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
            b = new Bitmap(Width, Height);
            Shapes = new Shape(Graphics.FromImage(b));
            cmdList = new CmdLists(Shapes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = textBox1.Lines;
            string[] lined = textBox2.Lines;

            try
            {
                if (lines.Length > 0)
                {
                    parseCom(string.Join("\n", lines));
                }
                else if (lines.Length > 0)
                {
                    parseCom(string.Join("\n", lined));
                }
                else
                {
                    throw new ShapeCreatorException("Please enter a command.");
                }
            }
            catch (ShapeCreatorException x)
            {
                var message = x.Message;
                if (x.line != 0)
                {
                    message = message + " Line at " + x.line;
                }
                MessageBox.Show(message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Refresh();
            
        }
        


      
        //Open a file and display in the textbox
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                string textUse = File.ReadAllText(ofd.FileName);
                textBox1.Text = textUse;
            }

        }

        //Button saves the text in the textbox to a file in specified path 
        private void button1_Click_1(object sender, EventArgs e)
        {
            StreamWriter writeIt = new StreamWriter(@"P:\My Documents\Desktop\ASE DEMO\SaveTxt.Txt");  //path to save file
            writeIt.Write(textBox2.Text);
            writeIt.Write(textBox1.Text);
            writeIt.Close();
        }

        //Button to clear form 
        private void button3_Click(object sender, EventArgs e)
        {
            FormSet.ClearForm(this);
        }

        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //display graphics on the bitmap
                Graphics g = e.Graphics;
                g.DrawImageUnscaled(b, 0, 0);
               
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //when enter is pressed the command is parsed
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
                cmdList.Parse(com); //parses the command entered using cmdList

            }

            catch (InvalidDataException e)
            {
                MessageBox.Show(e.Message, @"Error in Command"); //Displayed if there is an error
                   
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.EndsWith("run"))  //if the last line ends with run and enter is pressed 
                {
                    string TextLines = textBox1.Text.Trim().Substring(0, textBox1.Text.Length - 3);
                    string[] lines = TextLines.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); //lines are split to individual command lines
                    foreach (var line in lines)
                    {
                        
                            parseCom(line.Trim());
                            Refresh();
                      
                    }
                }
            }
        }
    }
}
