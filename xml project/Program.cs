using System;
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
        public void start(node n, ref string json,int m=1,int has_twin = 0, int twin_write = 0)
        {
           
            Console.WriteLine("{");
            json += "{" + Environment.NewLine;
            convert(n,ref json, m, has_twin, twin_write);
            Console.WriteLine("\n}");
            json += Environment.NewLine + "}" + Environment.NewLine;
        }

        public void convert(node n, ref string json, int m, int has_twin, int twin_write)
        {
            if ((n.children.Count == 0) && (has_twin == 0)) //has data //base case
            {
                //case 1 in table
                if ((n.type == 0) && (n.value == null))
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("\"" + n.name + "\":null");
                    json += "\"" + n.name + "\":null";
                }

                //case 3 in table
                else if ((n.type == 0) && (n.value != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"" + n.name + "\":{");
                    json += "\"" + n.name + "\":{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value);
                    json += "\"@" + v_name + "\":" + v_value + Environment.NewLine;
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("}");
                    json += "}";
                    //Console.Write("\"" + n.name+"\":{\"@"+v_name+"\":\""+v_value+"\"}");
                }
                //case 2 in table
                else if ((n.type == 1) && (n.value == null) && (n.data != null))
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("\"" + n.name + "\":\"" + n.data + "\"");
                    json += "\"" + n.name + "\":\"" + n.data + "\"";
                }

                //case 4 in table
                else if ((n.type == 1) && (n.value != null) && (n.data != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"" + n.name + "\":{");
                    json += "\"" + n.name + "\":{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"#text\":\"" + n.data + "\"");
                    json += "\"#text\":\"" + n.data + "\"" + Environment.NewLine;
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("}");
                    json += "}";
                    //Console.Write("\"" + n.name + "\":{\"@" + v_name + "\":\"" + v_value + "\",\"#text\":\"" + n.data);
                }
                else if ((n.type == 1) && (n.value != null) && (n.data == null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"" + n.name + "\":{");
                    json += "\"" + n.name + "\":{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("}");
                    json += "}";
                    //Console.Write("\"" + n.name + "\":{\"@" + v_name + "\":\"" + v_value + "\",\"#text\":\"" + n.data);
                }
                return;
            }

            else if ((n.children.Count == 0) && (has_twin == 1)) //has data //base case
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                //case 1 in table
                if ((n.type == 0) && (n.value == null))
                {
                    Console.Write("null");
                    json += "null";
                }

                //case 3 in table
                else if ((n.type == 0) && (n.value != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("{");
                    json += "{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value);
                    json += "\"@" + v_name + "\":" + v_value + Environment.NewLine;
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.Write("}");
                    json += "}";
                    //Console.Write("\"" + n.name+"\":{\"@"+v_name+"\":\""+v_value+"\"}");
                }
                //case 2 in table
                else if ((n.type == 1) && (n.value == null) && (n.data != null))
                { Console.Write("\"" + n.data + "\"");
                    json += "\"" + n.data + "\"";
                }
                //case 4 in table
                else if ((n.type == 1) && (n.value != null) && (n.data != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("{");
                    json += "{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"#text\":\"" + n.data + "\"");
                    json += "\"#text\":\"" + n.data + "\""+Environment.NewLine;
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }

                    Console.Write("}");
                    json += "}";
                    //Console.Write("\"" + n.name + "\":{\"@" + v_name + "\":\"" + v_value + "\",\"#text\":\"" + n.data);
                }

                else if ((n.type == 1) && (n.value != null) && (n.data == null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("{");
                    json += "{" + Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }

                    Console.Write("}");
                    json += "}";
                }

                return;
            }
            // if has children we should decide first if they are twins
            if ((n.children.Count > 1) && (n.children[0].name == n.children[1].name))
                has_twin = 1;
            else
            {
                has_twin = 0;
                //twin_write = 0;
            }

            //if children are not twins
            if ((has_twin == 0))
            {

                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                if (twin_write == 0)
                {
                    Console.Write("\"" + n.name + "\":");
                    json += "\"" + n.name + "\":";
                }
                Console.Write("{");
                json += "{";

                if (n.value != null)
                {
                    Console.Write("\n");
                    json += Environment.NewLine;
                    for (int j = 0; j < m + 1; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                }
                for (int i = 0; i < n.children.Count; i++)
                {
                    Console.Write("\n");
                    json += Environment.NewLine;
                    //if (n.value != null)
                    //{
                    //    string v_name, v_value;
                    //    int eq = n.value.IndexOf("=");
                    //    v_name = n.value.Substring(0, eq);
                    //    v_value = n.value.Substring(eq + 1);
                    //    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    //}
                    m++;

                    convert(n.children[i], ref json, m, has_twin, twin_write);
                    m--;
                    if (i != (n.children.Count - 1))
                    {
                        Console.Write(",");
                        json += ",";
                    }
                    if (i == (n.children.Count - 1))
                    {
                        Console.Write("\n");
                        json += Environment.NewLine;
                    }
                }
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                Console.Write("}");
                json += "}";
            }
            //if children are twins
            else if (has_twin == 1)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                if (twin_write == 0)
                {
                    Console.WriteLine("\"" + n.name + "\":{");
                    json += "\"" + n.name + "\":{" + Environment.NewLine;
                }

                else
                {   Console.WriteLine("{");
                json += "{" + Environment.NewLine;
                 }
                if (n.value != null)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(" ");
                        json += " ";
                    }
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    json += "\"@" + v_name + "\":" + v_value + "," + Environment.NewLine;
                }
                m++;
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                Console.Write("\"" + n.children[0].name + "\":[");
                json += "\"" + n.children[0].name + "\":[";

                twin_write = 1;

                for (int i = 0; i < n.children.Count; i++)
                {

                    Console.Write("\n");
                    json += Environment.NewLine;

                    m++;
                    convert(n.children[i], ref json, m, has_twin, twin_write);
                    m--;
                    if (i != (n.children.Count - 1))
                    { Console.Write(",");
                    json += ",";
                    }
                    if (i == (n.children.Count - 1))
                    { Console.Write("\n");
                        json += Environment.NewLine;
                            }
                }
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                m--;
                Console.WriteLine("]");
                json += "]" + Environment.NewLine;
                for (int j = 0; j < m; j++)
                {
                    Console.Write(" ");
                    json += " ";
                }
                Console.Write("}");
                json += "}";
            }
        }
    }
}
