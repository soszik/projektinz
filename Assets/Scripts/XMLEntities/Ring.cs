using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace XMlParser
{
    public class Ring
    {
        public enum direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public string right { get; set; }
        public float speed { get; set; }
        public string CreateNext { get; set; }
        //depending on createNext(if true)
        public string dir { get; set; }
        //depending on createNext(if true)
        public float[] placement { get; set; }
        public int group { get; set; }
    }
}
