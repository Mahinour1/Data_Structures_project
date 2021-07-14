using System;
using System.Collections.Generic;
using System.Text;

namespace xml_project
{
    class HeapNode
    {
        public char Data { get; set; }
        public uint Freq { get; set; }
        public HeapNode Left { get; set; }
        public HeapNode Right { get; set; }
        
    }
    
}
