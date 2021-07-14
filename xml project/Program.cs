using System;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        static void start(node n, int has_twin = 0, int twin_write = 0)
        {
            Console.WriteLine("{");
            convert(n, has_twin, twin_write);
            Console.WriteLine("\n}");
        }

        static void convert(node n, int has_twin, int twin_write)
        {
            if ((n.children.Count == 0) && (has_twin == 0)) //has data //base case
            {
                //case 1 in table
                if ((n.type == 0) && (n.value == null))
                    Console.Write("\"" + n.name + "\":null");


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

            else if ((n.children.Count == 0) && (has_twin == 1)) //has data //base case
            {
                //case 1 in table
                if ((n.type == 0) && (n.value == null))
                    Console.Write("null");


                //case 3 in table
                else if ((n.type == 0) && (n.value != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("{");
                    Console.WriteLine("\"@" + v_name + "\":" + v_value);
                    Console.Write("}");
                    //Console.Write("\"" + n.name+"\":{\"@"+v_name+"\":\""+v_value+"\"}");
                }
                //case 2 in table
                else if ((n.type == 1) && (n.value == null) && (n.data != null))
                    Console.Write("\"" + n.data + "\"");
                //case 4 in table
                else if ((n.type == 1) && (n.value != null) && (n.data != null))
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("{");
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    Console.WriteLine("\"#text\":\"" + n.data + "\"");
                    Console.Write("}");
                    //Console.Write("\"" + n.name + "\":{\"@" + v_name + "\":\"" + v_value + "\",\"#text\":\"" + n.data);
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
                if (twin_write == 0)
                    Console.Write("\"" + n.name + "\":");
                Console.Write("{");
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
                    convert(n.children[i], has_twin, twin_write);
                    if (i != (n.children.Count - 1))
                        Console.Write(",");
                    if (i == (n.children.Count - 1))
                        Console.Write("\n");

                }
                Console.Write("}");
            }
            //if children are twins
            else if (has_twin == 1)
            {
                if (twin_write == 0)
                    Console.WriteLine("\"" + n.name + "\":{");
                if (n.value != null)
                {
                    string v_name, v_value;
                    int eq = n.value.IndexOf("=");
                    v_name = n.value.Substring(0, eq);
                    v_value = n.value.Substring(eq + 1);
                    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                }
                Console.Write("\"" + n.children[0].name + "\":[");
                twin_write = 1;

                for (int i = 0; i < n.children.Count; i++)
                {

                    Console.Write("\n");
                    //if (n.value != null)
                    //{
                    //    string v_name, v_value;
                    //    int eq = n.value.IndexOf("=");
                    //    v_name = n.value.Substring(0, eq);
                    //    v_value = n.value.Substring(eq + 1);
                    //    Console.WriteLine("\"@" + v_name + "\":" + v_value + ",");
                    //}
                    convert(n.children[i], has_twin, twin_write);
                    if (i != (n.children.Count - 1))
                        Console.Write(",");
                    if (i == (n.children.Count - 1))
                        Console.Write("\n");
                }
                Console.WriteLine("]");
                Console.Write("}");
            }
        }
    }
}
