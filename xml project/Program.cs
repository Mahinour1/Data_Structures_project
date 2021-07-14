using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
			string[] lines = System.IO.File.ReadAllLines(@"D:\trial.txt");
			List<HeapNode> list = new List<HeapNode>();
			Frequency(lines, list);
			HuffmanBuild(ref list);
			
		}
        static void Frequency(string[] input, List<HeapNode> list)
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
						list.Add(node);
					}
					
                }
            }
        }

		static void HuffmanBuild(ref List<HeapNode> heap)
		{
			heap = heap.OrderBy(node => node.Freq).ToList();
			HeapNode Left, Right;
			while (heap.Count > 1)
			{
				Left = heap[0];
				Right = heap[1];
				heap.RemoveAt(0);
				heap.RemoveAt(0);
				HeapNode m = new HeapNode();
				m.Freq = Left.Freq + Right.Freq;
				m.Left = Left; m.Right = Right;
				heap.Add(m);
				heap = heap.OrderBy(node => node.Freq).ToList();

			}
		}
	}
}