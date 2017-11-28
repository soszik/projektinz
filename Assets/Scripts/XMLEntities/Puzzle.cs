using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XMlParser
{
    public class Puzzle
    {
        public Puzzle() { }
        //indicates the name of the file of puzzle prefab in default prefabs folder
        public String Name { get; set; }
        public List<File> Files { get; set; }
        public List<Part> Parts { get; set; }
        public List<SmallObject> SmallObjects { get; set; }

    }
}
