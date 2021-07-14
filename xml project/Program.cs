using System;

namespace xml_project
{
    class Program
    {
        static void Main(string[] args)
        {
           string[] lines = System.IO.File.ReadAllLines(@"C:\Users\ashraf\Desktop\Data_project\try.txt");
            Tree t=new Tree();
            t.drawtree(lines);
             t.Format();
            t.Minifying();
        }
    }
}
