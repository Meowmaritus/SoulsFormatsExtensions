using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        public struct Curve1
        {
            [XmlAttribute]
            public float Time;
            [XmlAttribute]
            public float Value;

            //public Curve1(float time, float value)
            //{
            //    Time = time;
            //    Value = value;
            //}
        }
    }
    
}
