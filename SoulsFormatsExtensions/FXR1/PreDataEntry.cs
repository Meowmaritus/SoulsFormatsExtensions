using SoulsFormats;
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
        public class PreDataEntry
        {
            [XmlAttribute]
            public int Unknown;

            public Param Data;
        }
    }
}
   
