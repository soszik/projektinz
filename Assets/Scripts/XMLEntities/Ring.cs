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
        public bool right { get; set; }
        public float speed { get; set; }
        public bool CreateNext { get; set; }
        //depending on createNext(if true)
        public direction dir { get; set; }
        //depending on createNext(if true)
        public Vector3 placement { get; set; }
        public int group { get; set; }
    }
}
