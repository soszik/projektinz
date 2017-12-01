using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMlParser
{
    public class Part
    {
        public String Type { set; get; }
        public String Id { set; get; }
        public float Z { get;  set; }
        public float Y { get;  set; }
        public float X { get; set; }
        public int Scale { get; set; }
    }
}
