using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XMlParser
{
    public class Scene
    {
        public Scene() { }

        public String Type { get; set; }
        public List<Puzzle> Puzzles { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }


    }

}
