using System;
using System.Collections.Generic;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
			string[] lines = System.IO.File.ReadAllLines(@"D:\trial.txt");
			//int error[lines.Length]
			int[] error= new int[lines.Length];
			CheckError(lines, error);

		}
		public static void CheckError(string[] c, int[] error)
		{
			string[] copy = new string[c.Length];
			for(int i=0;i<c.Length; i++)
            {
				copy[i] = c[i];
            }
			int flag=0;
			string name;
			Stack<string> s =new Stack<string>();
			int index = 0;
			string csub = c[0];
			string prevstring = null; int d; string x;
			while (index < c.Length)
			{
				int iter = csub.IndexOf('<');
				if (iter == -1 && csub.Length != 0) // line is only words
				{
					flag = 1;
					prevstring = prevstring + csub; // save these words in prevstring
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
						