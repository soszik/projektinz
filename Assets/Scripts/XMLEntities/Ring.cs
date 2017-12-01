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
        public string Id { get; set; }
        public string Right { get; set; }
        public float Speed { get; set; }
        public bool CreateNext { get; set; }
        //depending on createNext(if true)
        public direction Dir { get; set; }
        //depending on createNext(if true)
        public float[] Placement { get; set; }
        public int Group { get; set; }
        public int Scale { get; set; }
    }
}
