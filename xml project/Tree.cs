﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
	class Tree
	{
		public node root { get; set; }
		public int m = 0;
		public Tree(node root = null)
		{ }
		/*Tree()
		{
			root.name = null;
			root.value =null;
			root.data =null ;
			root.parent = null;
		}*/
		bool is_empty()
		{
			if (root == null)
				return true;
			return false;
		}
		
		public void drawtree(string[] c)
		{
			string mm;
			int index = 0;
			node currnode = new node();
			currnode = null;
			string csub = c[0];
			string prevstring = null; int d; string x;
			while (index < c.Length)
			{
				int iter = csub.IndexOf('<');
				if (iter == -1 && csub.Length != 0) // line is only words
				{
					prevstring = prevstring + csub; // save these words in prevstring
					index++; // go to next line
					if (index < c.Length) // check that file isn't finished
						csub = c[index];




				}
				else if (iter != -1)
				{
					if (csub[iter + 1] == '/') // closing tag
					{
						x = csub.Substring(0, iter); // check all characters before closing tag if there are any
						d = 0; // flag
						for (int i = 0; i < x.Length; i++)
						{
							if (x[i] != ' ')  // if any character is not space => there are words before closing tag
                            {
								d = 1;
								break;
							}
						}
						if (d == 1) // there are words before closing tag
							prevstring += x; // add these words to prevstring



						if (prevstring != null) // there are words in prevstring
						{
							currnode.data = prevstring; // save words in prevstring as data to currnode
							prevstring = null; // make prevstring null again
						}
						currnode = currnode.parent; // current tag is closed so we go back to parent of this tag
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
						node child = new node(); // create child node
						//if (currnode == null) // first tag
						//{
						//	//child.parent = null;
						//	root = child; // make root point at the first node
						//}



						child.parent = currnode; // now currnode is the parent of this child
						mm = csub.Substring(iter); // without spaces
						int close = mm.IndexOf('>');
						int space = mm.IndexOf(' ');
						int dash = mm.IndexOf('/');
						int open = mm.IndexOf('<');
						if (dash == -1 || dash > close || (dash < close && ((close - dash) > 1))) // not opening and closing at the same time
						{
							child.type = 1;
							if (space == -1 || space > close || (space < close && space < open)) // there is no value <tag>
								child.name = mm.Substring(1, close- 1); // set child name 
							else // there is value <tag ..>
							{
								child.name = mm.Substring(1, space-1); // set child name
								child.value = mm.Substring(space + 1, close - space - 1); // set child value
							}
							if (currnode != null)
								currnode.children.Add(child); // currnode is still parent of child, add child to parent's queue
							if (currnode == null) // first tag
							{
								root = child; // make root point at the first node
							}
							currnode = child; // make currnode = child because it will be parent of other tags coming
						}
						else // it's an opnening and closing tag
						{
							child.type = 0;
							if (space == -1 || space > close || (space < close && space < open)) // there is no value
								child.name = mm.Substring(1, dash- 1); // set child name
							else // there is value ex <tag type=""/>
							{
								child.name = mm.Substring(1, space- 1); // set child name
								child.value = mm.Substring(space + 1, dash - space - 1); // set child value
							}
							if (currnode != null)
								currnode.children.Add(child); // currnode is still parent of child, add child to parent's queue
																  // we didn't write currnode = child;
																  // because tag was closed .. parent is still currnode and is not updated with child => child won't be parent of other tags
							if (currnode == null) // first tag
							{
								root = child; // make root point at the first node
							}
						}



						csub = mm.Substring(close + 1); //
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
				}
			}



		}
		public void Format_Xml(node f, int m,ref string form)
		{
			// print_space(f);  
			for (int i = 0; i < m; i++)

			{
				Console.Write(" ");
				form += " ";
			}
			if (f.type == 0)/// print <find />
			{
				Console.Write("<" + f.name);
				form += "<" + f.name;
				if (f.value != null)
				{ Console.Write(" " + f.value + "/>" + "\n");
					form += " " + f.value + "/>" + Environment.NewLine;
				}
				else { Console.Write(" />" + "\n");
					form += " />" + Environment.NewLine;
						}
			}
			else
			{
				Console.Write("<" + f.name);
				form += "<" + f.name;
				if (f.value != null)
				{
					Console.Write(" " + f.value + ">");
					form += " " + f.value + ">";
				}

				else { Console.Write(">");
					form += ">";
				}
				Console.Write(f.data);
				form += f.data;
				List <node> child = f.children;
				if (f.children.Count != 0)
				{
					Console.Write("\n");
					form += Environment.NewLine;
					for (int i = 0; i < f.children.Count; i++)
					{
						m++;
						Format_Xml(child[i], m, ref form);
						m--;
					}

				}

				if (f.children.Count != 0)
				{
					//  print_space(f);
					for (int i = 0; i < m; i++)

					{
						Console.Write(" ");
						form += " ";
					}
				}

				Console.Write("</" + f.name + ">");
				form += "</" + f.name + ">";
				Console.Write("\n");
				form += Environment.NewLine;
			}


		}
		public void Format(ref string form)
		{
			Format_Xml(root, m, ref form);
		}
		public void Minifying(ref string min)
		{
			Minifying_Xml(root,ref min);
		}

		public void Minifying_Xml(node f, ref string min )
		{
			if (f.type == 0)//<find />
			{
				Console.Write("<" + f.name);
				min += "<" + f.name;
				if (f.value != null)
				{
					Console.Write(" " + f.value + " />");
					min += " " + f.value + " />";
				}
				else {
					Console.Write(" />");
					min += " />";
				}
			}
			else
			{
				Console.Write("<" + f.name);
				min += "<" + f.name;
				if (f.value != null)
				{
					Console.Write(" " + f.value + ">");
					min += " " + f.value + ">";
				}
				else
				{ Console.Write(">");
					min += ">";
				}
				Console.Write(f.data);
				min += f.data;
				List <node> child = f.children;
				if (f.children.Count != 0)
				{
					for (int i = 0; i < f.children.Count; i++)
					{
						Minifying_Xml(child[i],ref min);
					}
				}
				Console.Write("</" + f.name + ">");
				min += "</" + f.name + ">";
			}
		}

	}
}
