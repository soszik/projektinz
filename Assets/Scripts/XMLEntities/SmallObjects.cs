using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMlParser
{
    public class SmallObject
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public List<double[]> bezierPoints { get; set; }
        public int bezierSpeed { get; set; }

      public string vibrating { get; set; }
        public string pulsation { get; set; }
        public string rotate { get; set; }
        
        public double[] vibrationFrequency { get; set; }
        public double[] vibrationAmplitude { get; set; }

        public double[] pulsationFrequency { get; set; }
        public double[] pulsationAmplitudeMax { get; set; }
        public double[] pulsationAmplitudeMin { get; set; }

        public double[] rotationSpeed { get; set; }
        public double[] rotationMax { get; set; }
        public double[] rotationMin { get; set; }
        public double[] rotationDir { get; set; }
    }
}
