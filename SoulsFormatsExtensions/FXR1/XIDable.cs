﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        public abstract class XIDable
        {
            [XmlAttribute]
            public string XID;

            public virtual bool ShouldSerializeXID() => true;

            internal virtual void ToXIDs(FXR1 fxr)
            {

            }

            internal virtual void FromXIDs(FXR1 fxr)
            {

            }
        }
    }
}
