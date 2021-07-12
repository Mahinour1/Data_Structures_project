using System;
using System.Collections.Generic;
using System.Text;

namespace xml_project
{
	class Tree
	{
		public node root{ get; set; }
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
		/*	public node insert(node P, string n, string v = "NULL", string d = "NULL")
			{
				if (is_empty())
				{
					P = new node(n, v, d);
					return *P;
				}
				Node* Ch = new Node(n, v, d);
				P->children.push(Ch);
				return *Ch;
			}
			void draw_tree(string c [] )
			{
				Node* parents = new Node();
				int j = 0;
				Node parent;
				Node x;
				int len = sizeof(c);
				for (int i = 0; i < len + 1; i++)
				{
					//while()
					string head, name, value, data;
					int index_start, index_end, index_bigger;
					index_start = c[i].find("<");
					index_end = c[i].find("</");
					index_bigger = c[i].find(">");
					//string* level=new string();
					bool closed;




					if (index_end == -1)
					{
						data = "NULL";
						closed = false;
					}
					else
					{
						data = c[i].substr(index_bigger + 1, (index_end - index_bigger - 1));
						closed = true;
					}
					head = c[i].substr(index_start + 1, (c[i].find(">") - index_start - 1));
					int space = head.find(" ");
					name = head.substr(0, space - 1);
					if (space == -1)
						value = "NULL";
					else
						value = head.substr(space + 1);

					if (i == 0)
					{
						parent = insert(root, name, value, data);
						root = &parent;
						//root = add;
					}
					else
					{
						if (name != "/" + parent.name)
						{
							x = insert(&parent, name, value, data);
							if (index_end == -1)
							{
								parents[j] = parent;
								j++;
								parent = x;
							}
						}
						else
						{
							parent = parents[--j];
							//return step
						}


						//level[1] = name;
					}
					//else 
					//{
					//	if (index_end == -1)    //child to the previous line
					//	{
					//		insert(, name, value, data);

					//	}
					//	

					//	else
					//	{
					//		if(level[1])
					//	}
					//}
				}

			}*/
		public void drawtree(string[] c)
		{
			int index = 0;
			node currnode = new node() ;
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
							if (x[i] != ' ') d = 1; // if any character is not space => there are words before closing tag
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
						