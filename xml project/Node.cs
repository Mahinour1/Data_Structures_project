using System;
using System.Collections.Generic;
using System.Text;



namespace xml_project
{
    class node
    {

        public string name { get; set; }
        public string value { get; set; }
        public string data { get; set; }
        public int type { get; set; }
        //int type;
        //public Node[] children = new Node[10];
        public Queue<node> children = new Queue<node>();
        public node parent { get; set; }
        /* node(string n, string v = "NULL", string d = "NULL")
         {
             name = n;
             value = v;
             data = d;

             //type = t;
         }
         node()
         {
             name = "NULL";
             value = "NULL";
             data = "NULL";
             parent = null;
         }*/
    }

}
