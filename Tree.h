#pragma once
#include "Node.h"
#include <stack>
#include <string>
class Tree
{
public:
	Node* root;
	
	Tree(Node*root=NULL)
	{
		
		this->root = root;
		/*root.name = "NULL";
		root.value = "NULL";
		root.data = "NULL";*/
	}
	bool is_empty()
	{
		if (root == NULL)
			return true;
		return false;
	}
	public: Node insert(Node* P,string n,string v ="NULL",string d="NULL" )
	{
		if (is_empty())
		{
			P = new Node( n, v, d);
			return *P;
		}
		Node* Ch= new Node( n, v, d);
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
		for (int i = 0; i < len+1; i++)
		{
			//while()
			string head, name, value,data;
			int index_start, index_end,index_bigger;
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
			head = c[i].substr(index_start+1, (c[i].find(">")-index_start -1));
			int space = head.find(" ");
			name = head.substr(0,space-1);
			if (space == -1)
				value = "NULL";
			else
				value = head.substr(space + 1);
			
			if (i == 0)
			{
				parent=insert(root, name, value, data);
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
		
	}
};

