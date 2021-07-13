using System;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        static void start(node n)
        {
            Console.WriteLine("{");
            convert(n);
            Console.WriteLine("\n}");
        }
        static void convert(node n)
        {
            if (n.children.Count == 0) //has data //base case
            {
                //case 1 in table
                if ((n.type == 0) && (n.value == null))
                    Console.WriteLine("\"" + n.name + "\":null");


                //case 3 in table
                else if ((n.type == 0) && (n.value != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"" + n.name + "\":{");
                    Console.WriteLine("\"@" + v_name + "\":" + v_value);
                    Console.Write("}");
                    //Console.Write("\"" + n.name+"\":{\"@"+v_name+"\":\""+v_value+"\"}");
                }
                //case 2 in table
                else if ((n.type == 1) && (n.value == null) && (n.data != null))
                    Console.Write("\"" + n.name + "\":\"" + n.data + "\"");
                //case 4 in table
                else if ((n.type == 1) && (n.value != null) && (n.data != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"" + n.name + "\":{");
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    Console.WriteLine("\"#text\":\"" + n.data + "\"");
                    Console.Write("}");
                    //Console.Write("\"" + n.name + "\":{\"@" + v_name + "\":\"" + v_value + "\",\"#text\":\"" + n.data);
                }

                return;
            }

            // if has children

            Console.Write("\"" + n.name + "\":{");
            for (int i = 0; i < n.children.Count; i++)
            {
                Console.Write("\n");
                if (n.value != null)
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                }
                convert(n.children[i]);
                if (i != (n.children.Count - 1))
                    Console.Write(",");
                if (i == (n.children.Count - 1))
                    Console.Write("\n");

            }
            Console.Write("}");

        }
    }
}
