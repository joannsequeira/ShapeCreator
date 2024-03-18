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
using System.Threading;

namespace ShapeCreator
{
    public partial class Form1 : Form
    {

        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lines = textBox1.Text.Trim();
            string lined = textBox2.Text.Trim();

            threadParser("First Program: ", lines, lined);
        }

        private void threadParser(string prog, string lines, string lined)
        {
            //syntax check before run
            
            Boolean isSuccess = parseComSyn(prog, lines, lined, true, true);
            if (isSuccess)
            {

                Thread thread = new Thread(() =>
            {
                parseComSyn(prog, lines, lined, false, true);
            });
            thread.Start();
        }
            }

       

        private Boolean parseComSyn(string prog, string lines, string lined, Boolean syntaxtCheck)
        {
            return parseComSyn(prog, lines, lined, syntaxtCheck, false);
        }

        private Boolean parseComSyn(string prog, string lines, string lined, Boolean syntaxtCheck, Boolean fromRun)
        {
            try
            {
                if (lines != null && lines.Length > 0)
                {
                    parseCom(string.Join("\n", lines), syntaxtCheck);
                }
                else if (lined != null && lined.Length > 0)
                {
                    parseCom(string.Join("\n", lined), syntaxtCheck);
                }
                else
                {
                    throw new ShapeCreatorException("Please enter a command.");
                }

                if (!fromRun)
                {
                    MessageBox.Show(prog + (syntaxtCheck ? "No Syntax Error" : "Ran Successfully"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                }
                else if (!syntaxtCheck && fromRun)
                {
                    MessageBox.Show(prog + (syntaxtCheck ? "No Syntax Error" : "Ran Successfully"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                }
                return true;
            }
            catch (ShapeCreatorException x)
            {
                var message = x.Message;
                if (x.line != 0)
                {
                    message = message + " Line at " + x.line;
                }
                MessageBox.Show(prog + message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception x)
            {
                MessageBox.Show(prog + x.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }





        private void button5_Click(object sender, EventArgs e)
        {
            threadParser("Second Program:",textBox3.Text.Trim(),null);

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
                /* Graphics g = e.Graphics;
                g.DrawImageUnscaled(b, 0, 0); */
               
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //when enter is pressed the command is parsed
            if(e.KeyCode == Keys.Enter)
            {
                var com = textBox2.Text;
                if (string.IsNullOrEmpty(com))
                    return;
                threadParser("First Program: ", com, null);
                textBox2.Text = " ";
                Refresh();

            }
        }

        public void parseCom(string com, Boolean syntaxCheck)
        {
            try
            {
                Graphics g = pictureBox1.CreateGraphics();
                Shape Shapes = new Shape(g, syntaxCheck);

                CmdLists cmdList = new CmdLists(Shapes);
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

                        threadParser("First Program:", line.Trim(), null);
                        Refresh();
                      
                    }
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            parseComSyn("Second Program: ", textBox3.Text.Trim(), null, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string lines = textBox1.Text.Trim();
            string lined = textBox2.Text.Trim();

            parseComSyn("First Program: ", lines, lined, true);
        }


    }
}
