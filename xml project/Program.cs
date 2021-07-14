using System;
using System.Collections.Generic;
using System.Numerics;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
			string[] lines = System.IO.File.ReadAllLines(@"D:\trial.txt");
			Queue<HeapNode> queue = new Queue<HeapNode>();
			Frequency(lines, queue);
			Queue<HeapNode> copy = new Queue<HeapNode>();
			copy = queue;
			while (copy.Count != 0)
			{
				Console.WriteLine(copy.Peek().Data + " " + copy.Peek().Freq);
				copy.Dequeue();
			}
			
		}
		
        static void Frequency(string[] input, Queue<HeapNode> q)
        {
			string[] copy = new string[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				copy[i] = input[i];
			}
            for (int i = 0; i < copy.Length; i++)
            {
                for (int j = 0; j < copy[i].Length; j++)
                {
                    char compare = (copy[i])[j];
                    uint freq = 0;
                    for (int k = 0; k < copy.Length; k++)
                    {
                        for (int f = 0; f < copy[k].Length; f++)
                        {
							if ( compare !='0' && (copy[k])[f] != '0')
							{
								if ((copy[k])[f] == compare)
								{
									freq++;
									string n = copy[k].Substring(0, f) + "0" + copy[k].Substring(f + 1);
									copy[k] = n;
								}
							}
                           
                        }
                    }
					if (compare != '0')
					{
						HeapNode node = new HeapNode();
						node.Data = compare;
						node.Freq = freq;
						q.Enqueue(node);
					}
					
                }
            }
        }
       
	}
}