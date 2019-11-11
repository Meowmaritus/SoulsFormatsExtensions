using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        public struct Curve3
        {
            [XmlAttribute]
            public float Time;
            [XmlAttribute]
            public float X;
            [XmlAttribute]
            public float Y;
            [XmlAttribute]
            public float Z;

            //public Curve3(float time, float value1, float value2, float value3)
            //{
            //    Time = time;
            //    Value1 = value1;
            //    Value2 = value2;
            //    Value3 = value3;
            //}
        }
    }
    
}
