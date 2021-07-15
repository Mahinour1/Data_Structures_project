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
			textBox1.ScrollBars = ScrollBars.Both;
			textBox2.ScrollBars = ScrollBars.Both;
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//OpenFileDialog ofd = new OpenFileDialog();
			ofd1.ShowDialog();
			string x = ofd1.FileName;
			string[] lines1 = System.IO.File.ReadAllLines(@x);
			int[] error = new int[lines1.Length];
			string[] copy = new string[lines1.Length];

			string before = null;
			CheckError(lines1, error, copy);

			for (int i = 0; i < lines1.Length; i++)
			{
				/*Label l=new Label();
				l.Text = "Erorr";
				l.ForeColor = Color.Red;
				l.Font = new Font(Font.FontFamily, 14);
				*/
				if (error[i] == 1)
				{
				lines1[i]= lines1[i]+"   "+char.ConvertFromUtf32(8595)+"//Error";
				}
				before += lines1[i] + Environment.NewLine;

			}
			textBox1.Text = before;
			/*string y=textBox1.Lines[0];
			
			for (int i = 0; i < lines1.Length; i++) 
			{int finish= copy[i].Length;
				textBox1.Select(0,finish);
				//textBox1.BackColor(copy[i]);
			}*/
		}
		public static void CheckError(string[] c, int[] error, string[] copy)
		{
			//string[] copy = new string[c.Length];
			for (int i = 0; i < c.Length; i++)
			{
				copy[i] = c[i];
			}
			string mm;
			int flag = 0;
			string name;
			Stack<string> s = new Stack<string>();
			int index = 0;
			string csub = c[0];
			int d;
			while (index < c.Length)
			{
				int iter = csub.IndexOf('<');
				if (iter == -1 && csub.Length != 0) // line is only words
				{
					flag = 1;

					index++; // go to next line
					if (index < c.Length) // check that file isn't finished
						csub = c[index];
				}
				else if (iter != -1)
				{
					if (csub[iter + 1] == '/') // closing tag  dhgdhdgteh </tag>
					{
						int close = csub.IndexOf('>');
						int dash = csub.IndexOf('/');
						name = csub.Substring(dash + 1, close - dash - 1);
						if (name == s.Peek()) // no error
						{
							s.Pop();
						}
						else
						{
							error[index] = 1;
							while (s.Peek() != name)
							{
								copy[index - 1] += "</" + s.Pop() + ">";
							}
							s.Pop();
						}
						csub = csub.Substring(csub.IndexOf('>') + 1); // cut after '>'
						if (csub.Length == 0) // there's nothing after closing tag
						{
							index++; // go to next line
							if (index < c.Length)
								csub = c[index];
						}
						else // may be there are spaces only after closing tag => we don't need them
						{
							d = 0; // flag
							for (int i = 0; i < csub.Length; i++)
							{
								if (csub[i] != ' ')
								{
									d = 1;
									break;
								}
							}
							if (d == 0) // all characters are spaces
							{
								index++; // go to next line
								if (index < c.Length)
									csub = c[index];
							}
						}
					}
					else if (csub[iter + 1] != '/') // opening tag
					{
						if (flag != 1)
						{
							mm = csub.Substring(iter); // without spaces
							int close = mm.IndexOf('>');
							int space = mm.IndexOf(' ');
							int dash = mm.IndexOf('/');
							int open = mm.IndexOf('<');
							if (dash == -1 || dash > close || (dash<close && ((close-dash)>1) )) // not opening and closing at the same time
							{
								if (space == -1 || space > close || (space < close && space < open)) // there is no value <tag>
									name = mm.Substring(1, close - 1); // set child name
								else // there is value <tag ..>
								{
									name = mm.Substring(1, space - 1); // set child name
								}
								s.Push(name);
							}
							csub = mm.Substring(close + 1);
							if (csub.Length == 0) // there's nothing after tag
							{
								index++; // go to next line
								if (index < c.Length)
									csub = c[index];
							}
							else // may be there are spaces only after tag => we don't need them
							{
								d = 0; // flag
								for (int i = 0; i < csub.Length; i++)
								{
									if (csub[i] != ' ')
									{
										d = 1;
										break;
									}
								}
								if (d == 0) // all characters are spaces
								{
									index++; // go to next line
									if (index < c.Length)
										csub = c[index];
								}
							}
						}
						else if (flag == 1)
						{
							error[index] = 1;
							copy[index] += "</" + s.Pop() + ">";
							flag = 0;
						}

					}
				}
			}
			while (s.Count != 0)
			{
				error[index - 1] = 1;
				copy[index - 1] += "</" + s.Pop() + ">";
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			string x = ofd1.FileName;
			string[] lines1 = System.IO.File.ReadAllLines(@x);
			int[] error = new int[lines1.Length];
			string[] copy = new string[lines1.Length];
			string z = null;
			int flag = 0;

			CheckError(lines1, error, copy);
			for (int j = 0; j < lines1.Length; j++)
			{
				if (error[j] == 1)
				{
					flag = 1;
					break;
				}
			}
			if (flag == 1)
			{
				for (int i = 0; i < lines1.Length; i++)
				{
					z += copy[i] + Environment.NewLine;
				}
				textBox2.Text = z;
			}
            else
			{ 
				textBox2.Text = "The XML is already correct"; 
			}
		}

        private void button3_Click(object sender, EventArgs e)
        {
			//OpenFileDialog ofd = new OpenFileDialog();
			//ofd.ShowDialog();
			string x = ofd1.FileName;
			string[] lines1 = System.IO.File.ReadAllLines(@x);
			int[] error = new int[lines1.Length];
			string[] copy = new string[lines1.Length];
			CheckError(lines1, error, copy);
			string form = null;
			Tree T = new Tree();
			T.drawtree(copy);
			T.Format(ref form);	
			textBox2.Text =form ;
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.ShowDialog();
			string x = sfd.FileName;
			StreamWriter txt = new StreamWriter(@x);
			txt.WriteLine (textBox2.Text);
			txt.Close();
		}

        private void button4_Click(object sender, EventArgs e)
        {
			Program P=new Program();
			string x = ofd1.FileName;
			string json = null;
			string[] lines1 = System.IO.File.ReadAllLines(@x);
			int[] error = new int[lines1.Length];
			string[] copy = new string[lines1.Length];
			CheckError(lines1, error, copy);
			Tree T = new Tree();
			T.drawtree(copy);
			P.start(T.root,ref json);
			string one = json;
			textBox2.Text = one;
	
		}
		
		
        private void button6_Click(object sender, EventArgs e)
        {
			string x = ofd1.FileName;
			string[] lines1 = System.IO.File.ReadAllLines(@x);
			string min = null;
			Tree T = new Tree();
			T.drawtree(lines1);
			T.Minifying(ref min);
		
			textBox2.Text = min;
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
		List<HeapNode> ln;
        private void button7_Click(object sender, EventArgs e)
        {
			Program P=new Program();
			List<HeapNode> list = new List<HeapNode>();
			string[] lines = textBox2.Lines;
			P.Frequency(lines, list);
			P.HuffmanBuild(ref list);
			string stream = null;
			List<Characters> Bits = new List<Characters>();
			P.HuffmanTraverse(list[0], ref Bits, ref stream);
			string encoded = null;
			P.Encoding(lines, Bits, ref encoded);
			ln = list;
			//Console.WriteLine(encoded);
			textBox2.Text = encoded;
			
		}

        private void button8_Click(object sender, EventArgs e)
        {
			Program P = new Program();
			//List<HeapNode> list = new List<HeapNode>();
		
			//Console.WriteLine(encoded);
			string encoded = textBox2.Text;
			
			string decoded = P.Decoding(ln[0], encoded);
			//Console.WriteLine(decoded);
			textBox2.Text = decoded;
			
		}
    }
    
}
