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
		else // may be there are spaces only after closing tag => we don't need them
						{
							d = 0; // flag
							for (int i = 0; i < csub.Length; i++)
							{
								if (csub[i] != ' ') d = 1;
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
						node child=new node(); // create child node
						if (index == 0) // first tag
						{
							child.parent = null;
							root = child; // make root point at the first node
						}



						child.parent = currnode; // now currnode is the parent of this child
						int close = csub.IndexOf('>');
						int space = csub.IndexOf(' ');
						int dash = csub.IndexOf('/');
						if (dash == -1 || dash > close) // not opening and closing at the same time
						{
							if (space == -1 || space > close) // there is no value <tag>
								child.name = csub.Substring(iter + 1, close - iter - 1); // set child name
							else // there is value <tag ..>
							{
								child.name = csub.Substring(iter + 1, space - iter - 1); // set child name
								child.value = csub.Substring(space + 1, close - space - 1); // set child value
							}
							if (index != 0)
								currnode.children.Enqueue(child); // currnode is still parent of child, add child to parent's queue
							currnode = child; // make currnode = child because it will be parent of other tags coming
						}
						else // it's an opnening and closing tag
						{
							if (space == -1 || space > close) // there is no value
								child.name = csub.Substring(iter + 1, dash - iter - 1); // set child name
							else // there is value ex <tag type=""/>
							{
								child.name = csub.Substring(iter + 1, space - iter - 1); // set child name
								child.value = csub.Substring(space + 1, dash - space - 1); // set child value
							}
							if (index != 0)
								currnode.children.Enqueue(child); // currnode is still parent of child, add child to parent's queue
															   // we didn't write currnode = child;
															   // because tag was closed .. parent is still currnode and is not updated with child => child won't be parent of other tags
						}



						csub = csub.Substring(close + 1); //
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
								if (csub[i] != ' ') d = 1;
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
	}
}
					