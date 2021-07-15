﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
		public void Frequency(string[] input, List<HeapNode> list)
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
							if (compare != 'ى' && (copy[k])[f] != 'ى')
							{
								if ((copy[k])[f] == compare)
								{
									freq++;
									string n = copy[k].Substring(0, f) + "ى" + copy[k].Substring(f + 1);
									copy[k] = n;
								}
							}

						}
					}
					if (compare != 'ى')
					{
						HeapNode node = new HeapNode();
						node.Data = compare;
						node.Freq = freq;
						list.Add(node);
					}

				}
			}
		}

	public void HuffmanBuild(ref List<HeapNode> heap)
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

		public void HuffmanTraverse(HeapNode Root, ref List<Characters> bits, ref string stream)
		{
			if (Root.Left == null && Root.Right == null)
			{
				Characters element = new Characters();
				element.Data = Root.Data;
				element.Bits = stream;
				bits.Add(element);
				stream = stream.Substring(0, stream.Length - 1);
				return;
			}
			stream += '0';
			HuffmanTraverse(Root.Left, ref bits, ref stream);
			stream += '1';
			HuffmanTraverse(Root.Right, ref bits, ref stream);
			if (stream.Length != 0)
				stream = stream.Substring(0, stream.Length - 1);
			return;
		}
		public void Encoding(string[] lines, List<Characters> bits, ref string encoded)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					char curr = (lines[i])[j];
					encoded += bits[bits.FindIndex(x => x.Data == (lines[i])[j])].Bits;
				}
			}
		}
		public string Decoding(HeapNode root, string encoded)
		{
			string decoded = null; int index = 0; HeapNode curr = root;
			while (index < encoded.Length)
			{
				while (curr.Left != null && curr.Right != null && index < encoded.Length)
				{
					if (encoded[index] == '0')
						curr = curr.Left;
					else if (encoded[index] == '1')
						curr = curr.Right;
					index++;
				}
				decoded += curr.Data;
				curr = root;
			}
			return decoded;
		}
	}
}
