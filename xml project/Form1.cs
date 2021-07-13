using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;

namespace WindowsFormsApp1
{
 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            ofd1.ShowDialog();
            string x = ofd1.FileName;
            string[] lines1 = System.IO.File.ReadAllLines(@x);
            for(int i = 0; i < lines1.Length; i++)
            {
                textBox1.Text = lines1[i];
            }
           
           /*  string x;
             Vector<string> lines;
             //string[] lines =new string[100];

             OpenFileDialog ofd = new OpenFileDialog();
             if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             {

                // textBox1.Text = ofd.FileName;
               // x = ofd.FileName;
                 StreamReader sr = new StreamReader(File.OpenRead(ofd.FileName));

                 string line;
                 int i= 0;
                 //int j = 0;
                 do
                 {

                     line = sr.ReadLine();
                     lines[i] = line;
                     i++;

                 }
                 while (line != null);

                 //textBox2.Text = lines[2];
                 //string[]lines= System.IO.File.ReadAllLines(@"ofd.FileName");

              for(int j=0;j<lines.Length;j++)
                 { textBox1.Text = lines[j];

                 }

             }*/

        }
    }
}
