using System;
using System.Collections.Generic;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
			string[] lines = System.IO.File.ReadAllLines(@"D:\trial.txt");
			Tree t = new Tree();
			t.drawtree(lines);
			int[] error= new int[lines.Length];
			string[] copy = new string[lines.Length];
			//CheckError(lines, error, copy);
			for (int i = 0; i < lines.Length; i++)
			{
				Console.WriteLine(copy[i]); //Console.WriteLine();
			}

			

		}
		public static void CheckError(string[] c, int[] error, string[] copy)
		{
			//string[] copy = new string[c.Length];
			for(int i=0;i<c.Length; i++)
            {
				copy[i] = c[i];
            }
			string mm;
			int flag=0;
			string name;
			Stack<string> s =new Stack<string>();
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
							if (dash == -1 || dash > close) // not opening and closing at the same time
							{
								if (space == -1 || space > close || (space < close && space < open)) // there is no value <tag>
									name = mm.Substring(1, close- 1); // set child name
								else // there is value <tag ..>
								{ 
									name = mm.Substring(1, space-1); // set child name
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
							error[index - 1] = 1;
							copy[index - 1] += "</" + s.Pop() + ">";
							flag = 0;
						}

					}
				}
			}
		}
	}
}