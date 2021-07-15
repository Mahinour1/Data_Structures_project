using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    class HeapNode
    {
        public char Data { get; set; }
        public uint Freq { get; set; }
        public HeapNode Left { get; set; }
        public HeapNode Right { get; set; }
        
    }
    class Characters
    {
        public char Data { get; set; }
        public string Bits { get; set; }
    }
}
