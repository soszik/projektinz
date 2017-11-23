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
        public direction dir { get; set; }
        public Vector3 placement { get; set; }
        public int group { get; set; }
    }
}
